using Domain.ContactSection;
using Domain.IndividualSection;
using Domain.MediaSection;
using Domain.OrganizationSection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserSection
{
    /// <summary>
    /// User table
    /// </summary>
    public class User : IdentityUser<Guid>
    {

        /// <summary>
        /// Identity Individual or Organization
        /// </summary>
        /// <value></value>
        public string Discriminator { get; set; }

        /// <summary>
        /// If deleted or not
        /// </summary>
        /// <value></value>
        public bool Deleted { get; set; }

        /// <summary>
        /// If activated or not by the staff of ECO
        /// </summary>
        /// <value></value>
        public bool? Activated { get; set; }

        /// <summary>
        /// Accept terms and conditions
        /// Must accept condition else cannot use platform
        /// </summary>
        /// <value></value>
        public bool AgreeToTermsAndCondition { get; set; }
        
        /// <summary>
        /// Additional information about this user
        /// </summary>
        /// <value></value>
        public UserAdditionalDetail AdditionalDetail { get; set; }

        /// <summary>
        /// User media e.g bg picture, profile picture etc.
        /// </summary>
        /// <value></value>
        public UserMedia UserMedia { get; set; }

        /// <summary>
        /// List of roles for this user in the application
        /// </summary>
        /// <value></value>
        public ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// User Contacts
        /// </summary>
        /// <value></value>
        public ICollection<ApplicationContact> Contacts { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public User()
        {
            SetCollectionFields();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public User(string emailAddress)
        {
            Contacts = new HashSet<ApplicationContact>() {
                new ApplicationContact() {
                    Value = emailAddress,
                    Type = ApplicationContactType.emailAddress
                }
            };

            SetCollectionFields();
        }

        private void SetCollectionFields() 
        {
            UserRoles = new List<UserRole>();
            Contacts = new List<ApplicationContact>();
        }

        /// <summary>
        /// Return the full name of a user
        /// </summary>
        /// <returns></returns>
        public string GetFullName() {
            if (AdditionalDetail == null)
                throw new NullReferenceException("Additional Detail is null");

            var fullName= "";

            if (Discriminator.ToLower().Equals(nameof(Individual).ToLower())) {
                var other = string.IsNullOrEmpty(AdditionalDetail.OtherName) ? "" : AdditionalDetail.OtherName;
                fullName= $"{AdditionalDetail.LastName} {AdditionalDetail.FirstName} {other}".Trim();
            }

            if (Discriminator.ToLower().Equals(nameof(Organization).ToLower())) {
                fullName= $"{AdditionalDetail.OrganizationName}";
            }

            return fullName;
        }
    }
}
