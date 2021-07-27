using Domain.UserSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    /// <summary>
    /// Interface for handling email services
    /// </summary>
    public interface IApplicationEmailService
    {
        /// <summary>
        /// Send Email verification link to use to verify email 
        /// </summary>
        /// <param name="EmailServiceData">Data containing email information</param>
        /// <returns></returns>
        Task SendVerifyEmailLinkAsync(EmailServiceData EmailServiceData);

        /// <summary>
        /// Send a link for user to use and change password
        /// </summary>
        /// <param name="EmailServiceData">Data containing email information</param>
        /// <returns></returns>
        Task SendForgotPasswordLinkAsync(EmailServiceData EmailServiceData);
    }

    /// <summary>
    /// Data structure to be used when sending email
    /// </summary>
    public class EmailServiceData
    {
        /// <summary>
        /// User to send data to
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Token for verification if avaliable
        /// </summary>
        /// <value></value>
        public string Token { get; set; }

    }
}
