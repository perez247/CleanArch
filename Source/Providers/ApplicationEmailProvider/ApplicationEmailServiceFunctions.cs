using Application.Common.ApplicationHelperFunctions;
using Application.Exceptions;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationEmailProvider
{
    /// <summary>
    /// Email utility functions
    /// </summary>
    public static class ApplicationEmailServiceFunctions
    {
        /// <summary>
        /// Generate the neccessary information from the environment variable
        /// </summary>
        /// <returns></returns>
        public static ApllicationEmailServiceObject GenerateEmailObject()
        {
            // EMAIL_SERVER must contain information as = email|password|smtp|portNUmber
            var envAsString = EnvironemtUtilityFunctions.EMAIL_SERVER;

            if (string.IsNullOrEmpty(envAsString) || string.IsNullOrWhiteSpace(envAsString) || envAsString == "None")
                return new ApllicationEmailServiceObject() { CanSend = false };

            var envAsArray = envAsString.Split('|');

            return new ApllicationEmailServiceObject()
            {
                Sender = envAsArray[0],
                Password = envAsArray[1],
                SmtpServer = envAsArray[2],
                Port = Int32.Parse(envAsArray[3])
            };

        }

        /// <summary>
        /// Generate the neccessary information for sending the mail
        /// </summary>
        /// <param name="emailObjects">email object created</param>
        public static void CreateMimeMessage(ApllicationEmailServiceObject emailObjects)
        {

            var mime = new MimeMessage();

            if (!emailObjects.CanSend) { return; }

            mime.From.Add(new MailboxAddress("Company name", emailObjects.Sender));
            mime.Sender = new MailboxAddress("Company name", emailObjects.Sender);

            // Populate list with destination email and full name
            mime.To.AddRange(emailObjects.To.Select(x => new MailboxAddress("Name", x.Email)));

            mime.Subject = emailObjects.Subject;
            mime.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            { Text = emailObjects.Content };

            emailObjects.MimeMessage = mime;
        }

        /// <summary>
        /// Method for calling third party application to send mail
        /// </summary>
        /// <param name="_emailObjects"></param>
        /// <returns></returns>
        public static async Task SendData(ApllicationEmailServiceObject _emailObjects)
        {
            if (!_emailObjects.CanSend)
                throw new CustomMessageException(_emailObjects.Content);

            using (MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtpClient.ConnectAsync(_emailObjects.SmtpServer, 465, true);
                await smtpClient.AuthenticateAsync(_emailObjects.Sender, _emailObjects.Password);
                await smtpClient.SendAsync(_emailObjects.MimeMessage);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
