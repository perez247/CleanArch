using System.Collections.Generic;
using Domain.UserSection;

namespace Domain.IndividualSection
{
    /// <summary>
    /// An individual model
    /// </summary>
    public class Individual : User
    {
        /// <summary>
        /// Skills of this individual
        /// </summary>
        /// <value></value>
        public ICollection<IndividualSkill> Skills { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public Individual()
        {
            Skills = new List<IndividualSkill>();
        }

        /// <summary>
        /// Pass email address for the contact
        /// </summary>
        /// <param name="emailAddress"></param>
        public Individual(string emailAddress) : base(emailAddress)
        {}
    }
}