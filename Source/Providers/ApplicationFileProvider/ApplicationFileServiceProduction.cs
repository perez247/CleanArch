using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Common.ApplicationHelperFunctions;
using Application.Common.ApplicationValidations;
using Domain.FileSection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFileProvider
{
    /// <summary>
    /// S3 bucket implementation similar to AWS S3
    /// </summary>
    public class ApplicationFileServiceProduction
    {
        private readonly string S3_SECRET_KEY;
        private readonly string S3_ACCESS_KEY;
        private readonly string S3_HOST_ENDPOINT;
        private readonly string S3_BUCKET_NAME;
        private readonly IAmazonS3 _s3Client;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationFileServiceProduction()
        {
            var data = EnvironemtUtilityFunctions.DO_S3_BUCKET;
            var arrayData = data.Split('|');
            S3_SECRET_KEY = arrayData[0];
            S3_ACCESS_KEY = arrayData[1];
            S3_BUCKET_NAME = arrayData[2];
            S3_HOST_ENDPOINT = arrayData[3];

            AmazonS3Config ClientConfig = new AmazonS3Config();
            ClientConfig.ServiceURL = "https://" + S3_HOST_ENDPOINT;
            _s3Client = new AmazonS3Client(S3_ACCESS_KEY, S3_SECRET_KEY, ClientConfig);
        }

        private string DetermineDirectory(string extension)
        {
            if (extension == "png" || extension == "jpeg" || extension == "jpg")
                return "images";

            return "documents";
        }

        /// <summary>
        /// Uplaod Image to the cloud/server
        /// </summary>
        /// <param name="file">File to upload</param>
        /// <returns></returns>
        public async Task<AppFile> UploadFile(IFormFile file)
        {

            var outputStream = new MemoryStream();
            var extention = ApplicationFileValidation.GetExtension(file.ContentType);
            var directory = DetermineDirectory(extention);

            await file.CopyToAsync(outputStream);

            outputStream.Seek(0, SeekOrigin.Begin);

            var fileTransferUtility = new TransferUtility(_s3Client);
            var bucketPath = !string.IsNullOrWhiteSpace(directory)
                    ? S3_BUCKET_NAME + @"/" + directory
                    : S3_BUCKET_NAME;

            var fileName = Guid.NewGuid() + "." + extention;

            var fileUploadRequest = new TransferUtilityUploadRequest()
            {
                CannedACL = S3CannedACL.PublicRead,
                BucketName = bucketPath,
                Key = fileName,
                InputStream = outputStream
            };

            await fileTransferUtility.UploadAsync(fileUploadRequest);

            return new AppFile()
            {
                PublicId = fileName,
                Type = file.ContentType,
                PublicUrl = "https://" + S3_BUCKET_NAME + "." + S3_HOST_ENDPOINT + "/" + directory + "/" + fileName,
            };
        }

        /// <summary>
        /// Delete image from cloud/server
        /// </summary>
        /// <param name="file">File to delete</param>
        /// <returns></returns>
        public async Task DeleteFile(AppFile file)
        {
            if (file == null)
                return;

            if (string.IsNullOrEmpty(file.PublicId))
                return;

            var extention = ApplicationFileValidation.GetExtension(file.Type);
            var directory = DetermineDirectory(extention);

            var fileTransferUtility = new TransferUtility(_s3Client);
            var bucketPath = !string.IsNullOrWhiteSpace(directory)
                    ? S3_BUCKET_NAME + @"/" + directory
                    : S3_BUCKET_NAME;

            var deleteObject = new DeleteObjectRequest()
            {
                BucketName = bucketPath,
                Key = file.PublicId,
            };

            await fileTransferUtility.S3Client.DeleteObjectAsync(deleteObject);
        }


        /// <summary>
        /// Duplicate an image on the colud
        /// </summary>
        /// <param name="file">File to duplicate</param>
        /// <returns></returns>
        public async Task<AppFile> DuplicteFile(AppFile file)
        {

            if (file == null)
                return null;

            if (string.IsNullOrEmpty(file.PublicId))
                return null;

            var extention = ApplicationFileValidation.GetExtension(file.Type);
            var directory = DetermineDirectory(extention);

            var fileTransferUtility = new TransferUtility(_s3Client);
            var bucketPath = !string.IsNullOrWhiteSpace(directory)
                    ? S3_BUCKET_NAME + @"/" + directory
                    : S3_BUCKET_NAME;

            var destinationFile = Guid.NewGuid() + "." + Path.GetExtension(file.PublicId);

            CopyObjectRequest request = new CopyObjectRequest
            {
                SourceBucket = bucketPath,
                SourceKey = file.PublicId,
                DestinationBucket = bucketPath,
                DestinationKey = destinationFile
            };

            CopyObjectResponse response = await fileTransferUtility.S3Client.CopyObjectAsync(request);

            return new AppFile()
            {
                PublicId = destinationFile,
                Type = file.Type,
                PublicUrl = "https://" + S3_BUCKET_NAME + "." + S3_HOST_ENDPOINT + "/" + directory + "/" + destinationFile,
            };
        }
    }
}
