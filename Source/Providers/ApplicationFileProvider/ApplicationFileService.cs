using Application.Interfaces.IServices;
using Domain.FileSection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFileProvider
{
    /// <summary>
    /// Service for handling all files
    /// </summary>
    public class ApplicationFileService : IApplicationFileService
    {
        /// <summary>
        /// Hosting environment 
        /// </summary>
        /// <value></value>
        private IWebHostEnvironment Ienv { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ienv">Ihostingenvironment</param>
        public ApplicationFileService(IWebHostEnvironment ienv)
        {
            Ienv = ienv;
        }

        /// <summary>
        /// Upload a file
        /// </summary>
        /// <param name="file">File to upload to the desired server/bucket</param>
        /// <returns></returns>
        public async Task<AppFile> UploadFile(IFormFile file)
        {
            if (!Ienv.IsProduction())
                return await ApplicationFileServiceDevelopment.UploadFile(file);

            var s3Service = new ApplicationFileServiceProduction();
            return await s3Service.UploadFile(file);
        }

        /// <summary>
        /// Upload multiple files
        /// </summary>
        /// <param name="files">List of files to upload to the desired server/bucke</param>
        /// <returns></returns>
        public async Task<ICollection<AppFile>> UploadMultipleFile(ICollection<IFormFile> files)
        {
            var uploadedFiles = new List<AppFile>();

            foreach (var file in files)
            {
                uploadedFiles.Add(await UploadFile(file));
            }
            return uploadedFiles;
        }

        /// <summary>
        /// Delete a file from the server/bucket
        /// </summary>
        /// <param name="file">a single file to delete</param>
        public async Task DeleteFile(AppFile file)
        {
            if (!Ienv.IsProduction())
                ApplicationFileServiceDevelopment.DeleteFile(file);
            else
            {
                var s3Service = new ApplicationFileServiceProduction();
                await s3Service.DeleteFile(file);
            }
        }

        /// <summary>
        /// Delete multiple files from the server/bucket
        /// </summary>
        /// <param name="files">list of files to delete</param>
        public async Task DeleteMultipleFiles(ICollection<AppFile> files)
        {
            foreach (var file in files)
            {
                await DeleteFile(file);
            }
        }

        /// <summary>
        /// Duplicate a single file
        /// </summary>
        /// <param name="file">single file to duplicate</param>
        /// <returns></returns>
        public async Task<AppFile> DuplicateFile(AppFile file)
        {
            if (!Ienv.IsProduction())
                return ApplicationFileServiceDevelopment.DuplicateFile(file);

            var s3Service = new ApplicationFileServiceProduction();
            return await s3Service.DuplicteFile(file);
        }

        /// <summary>
        /// Duplicate multiple files
        /// </summary>
        /// <param name="files">list of files to duplicate</param>
        /// <returns></returns>
        public async Task<ICollection<AppFile>> DuplicateMultipleFiles(ICollection<AppFile> files)
        {
            var uploadedFiles = new List<AppFile>();

            foreach (var file in files)
            {
                uploadedFiles.Add(await DuplicateFile(file));
            }
            return uploadedFiles;
        }
    }
}
