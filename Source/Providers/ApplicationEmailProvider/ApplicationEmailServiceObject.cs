using Domain.UserSection;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationEmailProvider
{
    /// <summary>
    /// Obect required for sending emails
    /// </summary>
    public class ApllicationEmailServiceObject
    {
        /// <summary>
        /// Flag to indicate that the message can be send after configuratuin
        /// </summary>
        /// <value></value>
        public bool CanSend { get; set; } = true;

        /// <summary>
        /// Sender email
        /// </summary>
        /// <value></value>
        public string Sender { get; set; }

        /// <summary>
        /// Password of the sender
        /// </summary>
        /// <value></value>
        public string Password { get; set; }

        /// <summary>
        /// Smtp server
        /// </summary>
        /// <value></value>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Port for sending the mail
        /// </summary>
        /// <value></value>
        public int Port { get; set; }

        /// <summary>
        /// List of people to send to
        /// </summary>
        /// <value></value>
        public ICollection<User> To { get; set; } = new HashSet<User>();

        /// <summary>
        /// Subject of the mail
        /// </summary>
        /// <value></value>
        public string Subject { get; set; }

        /// <summary>
        /// COntent of the message
        /// </summary>
        /// <value></value>
        public string Content { get; set; }

        /// <summary>
        /// Mime message
        /// </summary>
        /// <value></value>
        public MimeMessage MimeMessage { get; set; }
    }

}
