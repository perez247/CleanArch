using System;
using System.Net;
using Application.Interfaces.IExceptions;

namespace Application.Exceptions
{
    /// <summary>
    /// class responsible for throwing email not confirmed
    /// </summary>
    public class ConfirmEmailException : Exception, IGeneralException
    {
        /// <summary>
        /// Gets the email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="statusCode"></param>
        public ConfirmEmailException(string emailAddress, HttpStatusCode statusCode = HttpStatusCode.PreconditionRequired)
            : base($"\"{emailAddress}\" has not been confirmed")
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Status code to use so the application will understand
        /// </summary>
        /// <value></value>
        public HttpStatusCode StatusCode { get; set; }
    }
}