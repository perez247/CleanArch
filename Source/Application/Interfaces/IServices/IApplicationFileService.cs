using Domain.FileSection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    /// <summary>
    /// File interface service for handling Files
    /// </summary>
    public interface IApplicationFileService
    {
        /// <summary>
        /// Upload a file
        /// </summary>
        /// <param name="file">File to upload to the desired server/bucket</param>
        /// <returns></returns>
        Task<AppFile> UploadFile(IFormFile file);

        /// <summary>
        /// Upload multiple files
        /// </summary>
        /// <param name="files">List of files to upload to the desired server/bucke</param>
        /// <returns></returns>
        Task<ICollection<AppFile>> UploadMultipleFile(ICollection<IFormFile> files);

        /// <summary>
        /// Delete a file from the server/bucket
        /// </summary>
        /// <param name="file">a single file to delete</param>
        Task DeleteFile(AppFile file);

        /// <summary>
        /// Delete multiple files from the server/bucket
        /// </summary>
        /// <param name="files">list of files to delete</param>
        Task DeleteMultipleFiles(ICollection<AppFile> files);

        /// <summary>
        /// Duplicate a single file
        /// </summary>
        /// <param name="file">single file to duplicate</param>
        /// <returns></returns>
        Task<AppFile> DuplicateFile(AppFile file);

        /// <summary>
        /// Duplicate multiple files
        /// </summary>
        /// <param name="files">list of files to duplicate</param>
        /// <returns></returns>
        Task<ICollection<AppFile>> DuplicateMultipleFiles(ICollection<AppFile> files);
    }
}
