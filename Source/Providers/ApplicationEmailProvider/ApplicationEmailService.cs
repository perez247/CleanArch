using Application.Common.ApplicationHelperFunctions;
using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationEmailProvider
{
    /// <summary>
    /// Class that implemets the application email service interface
    /// </summary>
    public class ApplicationEmailService : IApplicationEmailService
    {
        /// <summary>
        /// Public url for connecting to the application
        /// </summary>
        private readonly string _hostname = EnvironemtUtilityFunctions.HOSTNAME;

        /// <summary>
        /// Object used for sending emails
        /// </summary>
        private readonly ApllicationEmailServiceObject _emailObjects = ApplicationEmailServiceFunctions.GenerateEmailObject();

        /// <summary>
        /// Path for storing all the email templates
        /// </summary>
        private readonly string _templatePath = "../Provider/ApplicationEmailProvider/EmailTemplates";

        /// <summary>
        /// Send a link for user to use and change password
        /// </summary>
        /// <param name="EmailServiceData">Data containing email information</param>
        /// <returns></returns>
        public async Task SendForgotPasswordLinkAsync(EmailServiceData EmailServiceData)
        {

            string token = WebUtility.UrlEncode(EmailServiceData.Token);
            string id = WebUtility.UrlEncode(EmailServiceData.User.Id.ToString());

            _emailObjects.To.Add(EmailServiceData.User);
            _emailObjects.Subject = "Reset Password";
            var url = $"{_hostname}/public/reset-password?token={token}&userId={id}";

            using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/ForgotPassword.html"))
            {
                var content = SourceReader.ReadToEnd();
                content = content.Replace("{1}", url);
                _emailObjects.Content = content;

                ApplicationEmailServiceFunctions.CreateMimeMessage(_emailObjects);

                await ApplicationEmailServiceFunctions.SendData(_emailObjects);
            }
        }

        /// <summary>
        /// Send Email verification link to use to verify email 
        /// </summary>
        /// <param name="EmailServiceData">Data containing email information</param>
        /// <returns></returns>
        public async Task SendVerifyEmailLinkAsync(EmailServiceData EmailServiceData)
        {
            string token = WebUtility.UrlEncode(EmailServiceData.Token);
            string id = WebUtility.UrlEncode(EmailServiceData.User.Id.ToString());

            _emailObjects.To.Add(EmailServiceData.User);
            _emailObjects.Subject = "Verify Email";
            var url = $"{_hostname}/public/verify-email?token={token}&userId={id}";

            using (StreamReader SourceReader = System.IO.File.OpenText(_templatePath + "/VerifyEmail.html"))
            {
                var content = SourceReader.ReadToEnd();
                content = content.Replace("{1}", url);
                _emailObjects.Content = content;

                ApplicationEmailServiceFunctions.CreateMimeMessage(_emailObjects);

                await ApplicationEmailServiceFunctions.SendData(_emailObjects);
            }
        }
    }
}
