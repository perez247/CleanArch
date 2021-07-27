using Application.Common.ApplicationHelperFunctions;
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
    /// Handle the functions of file in a development environment
    /// </summary>
    public static class ApplicationFileServiceDevelopment
    {
        /// <summary>
        /// Location for storing data locally if cloud if not avaliable or if this is not production
        /// </summary>
        private static readonly string _dataDir = "../Providers/ApplicationFileProvidern/FileLocalData";

        /// <summary>
        /// Upload a file locally
        /// </summary>
        /// <param name="file">File to upload to the desired server/bucket</param>
        /// <returns></returns>
        public static async Task<AppFile> UploadFile(IFormFile file)
        {

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fileName = Path.Combine(_dataDir, uniqueName);
            var apiPath = $"{EnvironemtUtilityFunctions.HOSTNAME}/api/file/document/{uniqueName}";

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new AppFile()
            {
                Name = file.Name,
                Type = file.ContentType,
                PublicId = uniqueName,
                PublicUrl = apiPath
            };
        }

        /// <summary>
        /// Delete a file locally
        /// </summary>
        /// <param name="appFile">a single file to delete</param>
        public static void DeleteFile(AppFile appFile)
        {
            if (string.IsNullOrEmpty(appFile.PublicId))
                return;

            var path = Path.Combine(_dataDir, appFile.PublicId);

            if (!File.Exists(path))
                return;

            File.Delete(path);
        }

        /// <summary>
        /// Duplicate a single file locally
        /// </summary>
        /// <param name="file">single file to duplicate</param>
        /// <returns></returns>
        public static AppFile DuplicateFile(AppFile file)
        {
            var fileToCopy = Path.Combine(_dataDir, file.PublicId);

            if (!File.Exists(fileToCopy))
                return null;

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
            var fileName = Path.Combine(_dataDir, uniqueName);
            var apiPath = $"{EnvironemtUtilityFunctions.HOSTNAME}/api/file/document/{uniqueName}";

            File.Copy(fileToCopy, fileName, true);

            return new AppFile()
            {
                Name = file.Name,
                Type = file.Type,
                PublicId = uniqueName,
                PublicUrl = apiPath
            };
        }
    }
}
