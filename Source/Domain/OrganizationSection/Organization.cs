using System;
using Domain.UserSection;

namespace Domain.OrganizationSection
{
    /// <summary>
    /// Organization
    /// </summary>
    public class Organization : User
    {
        /// <summary>
        /// Used to Categorize Organizations in one branch
        /// </summary>
        /// <value></value>
        public Guid HeadQuarterId { get; set; }

        /// <summary>
        /// Determines if this branch accept request from other branches to join
        /// </summary>
        /// <value></value>
        public bool AcceptRequestFromOrganization { get; set; }

        /// <summary>
        /// Determines if this branch accept request from individuals to join as employees
        /// </summary>
        /// <value></value>
        public bool AcceptRequestFromIndividual { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Organization()
        {
        }

        /// <summary>
        /// Pass email address for the contact
        /// </summary>
        /// <param name="emailAddress"></param>
        public Organization(string emailAddress) : base(emailAddress)
        {}
    }
}