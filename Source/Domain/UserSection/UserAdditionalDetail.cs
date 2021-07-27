using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BaseClasses;

namespace Domain.UserSection
{
    /// <summary>
    /// More information about the user
    /// </summary>
    public class UserAdditionalDetail : BaseUpdate
    {
        // Id of the Table

        /// <summary>
        /// Id of the user detail
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// User this additional infor is connected to
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Id of the User it is connected to
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        /// <summary>
        /// About me field
        /// </summary>
        /// <value>string</value>
        public string About { get; set; }

        /// <summary>
        /// Ads display localized
        /// </summary>
        /// <value></value>
        public bool DisplayLocalAds { get; set; }

        /// <summary>
        /// If the theme should be dark mode
        /// </summary>
        /// <value></value>
        public bool DarkMode { get; set; }

        /// <summary>
        /// If the user is a new user
        /// </summary>
        /// <value></value>
        public bool NewUser { get; set; }

        // Individual Details --------------------------------------------------------------------------------

        /// <summary>
        /// First name of the Individual or staff
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Individual or staff
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }

        /// <summary>
        /// Other name of the Individual or staff
        /// </summary>
        /// <value></value>
        public string OtherName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gender of the User
        /// m = Male, f = Female, anyother = other
        /// </summary>
        /// <value></value>
        public string Gender { get; set; }

        // End of Individual ---------------------------------------------------------------------------------

        // Organization Details ------------------------------------------------------------------------------

        /// <summary>
        /// Name of the Organization
        /// </summary>
        /// <value></value>
        public string OrganizationName { get; set; }

        /// <summary>
        /// What type of Organization is this
        /// </summary>
        /// <value></value>
        public OrganizationType OrganizationType { get; set; }

        /// <summary>
        /// Year this company was found
        /// </summary>
        /// <value></value>
        public int? YearEstablished { get; set; } = 0;

        /// <summary>
        /// Website of the organization
        /// </summary>
        /// <value></value>
        public string Website { get; set; }

        /// <summary>
        /// Category of industry
        /// </summary>
        /// <value></value>
        public string Industry { get; set; }

        /// <summary>
        /// If this organization is a branch or not
        /// </summary>
        /// <value></value>
        public BranchStatus BranchStatus { get; set; }

        // End of Organization -------------------------------------------------------------------------------

        // Staff Details -------------------------------------------------------------------------------------

        /// <summary>
        /// Position of the staff
        /// </summary>
        /// <value></value>
        public string Position { get; set; }

        /// <summary>
        /// Department of the staff
        /// </summary>
        /// <value></value>
        public string Department { get; set; }

        // End of staff --------------------------------------------------------------------------------------

        /// <summary>
        /// Constructor
        /// </summary>
        public UserAdditionalDetail()
        {
            NewUser = true;

            OrganizationType = OrganizationType.unknown;
            BranchStatus = BranchStatus.Independent;
        }
    }

    /// <summary>
    /// Types of Organization in this application
    /// Organizations for this platform should contain people more than 500 employes
    /// </summary>
    public enum OrganizationType
    {

        /// Small medium business/enterprise
        sme,

        /// Charities
        charity,

        /// NGO
        ngo,

        /// Unknown
        unknown,
    }

    /// <summary>
    /// Status of the branch connection
    /// </summary>
    public enum BranchStatus
    {
        /// Accepted
        Accepted,

        /// Rejected
        Rejected,

        /// Request Sent
        RequestSent,

        /// A stand alone organization
        Independent,

        /// Removed
        Removed
    }

}
