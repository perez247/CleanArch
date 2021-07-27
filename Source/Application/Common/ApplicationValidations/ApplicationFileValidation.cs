using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Common.ApplicationValidations
{
    /// <summary>
    /// Utility for validating files
    /// </summary>
    public static class ApplicationFileValidation
    {
        /// <summary>
        /// List of allowed content types in this application
        /// </summary>
        private static readonly List<string> AllowedContentType = new List<string>() {
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "application/msword",
            "application/pdf",
            "image/png",
            "image/jpg",
            "image/jpeg"
        };

        /// <summary>
        /// Pair each content type with the right extension
        /// </summary>
        private static readonly List<KeyValuePair<string, string>> FileTypes = new List<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx"),
            new KeyValuePair<string, string>("application/msword", "doc"),
            new KeyValuePair<string, string>("application/pdf", "pdf"),
            new KeyValuePair<string, string>("image/png", "png"),
            new KeyValuePair<string, string>("image/jpg", "jpg"),
            new KeyValuePair<string, string>("image/jpeg", "jpeg"),
        };

        /// <summary>
        /// Get the file extension required
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string GetExtension(string fileType)
        {
            return FileTypes.Find(x => x.Key.ToLower() == fileType).Value;
        }


        /// <summary>
        /// Check if file is a picture
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool BeAValidPicture(IFormFile file)
        {
            var extention = GetExtension(file.ContentType);

            if (extention == "jpeg" || extention == "jpg" || extention == "png")
                return true;

            return false;
        }

        /// <summary>
        /// Only required File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool ValidFile(IFormFile file)
        {
            return ApplicationFileValidation.AllowedContentType.Contains(file.ContentType);
        }

        /// <summary>
        /// Only required File of not more than 2MB
        /// </summary>
        /// <param name="file"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool ValidFileSize(IFormFile file, int size = 2097152)
        {
            return file.Length < size;
        }

        /// <summary>
        /// Only required File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool ValidFileName(IFormFile file)
        {
            Regex r = new Regex("^[0-9A-Za-z.\\-_ ]*$");
            return r.Match(file.FileName).Success;
        }
    }
}
