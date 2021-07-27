using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BaseClasses;
using Domain.UserSection;

namespace Domain.ContactSection
{
    /// <summary>
    /// User contact used in this application
    /// </summary>
    public class ApplicationContact : BaseUpdate
    {
        /// <summary>
        /// Id of the Contact
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the user this contact is connected to
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        /// <summary>
        /// User this contact is connected to
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// The type of Contact
        /// </summary>
        /// <value></value>
        public ApplicationContactType Type { get; set; }

        /// <summary>
        /// First phone number - Max of 128 characters
        /// </summary>
        /// <value></value>
        public string Value { get; set; }

    }

    /// <summary>
    /// The types of contact used in this application
    /// </summary>
    public enum ApplicationContactType
    {

        /// Phone Number
        phone,

        /// Email
        emailAddress,

    }
}
