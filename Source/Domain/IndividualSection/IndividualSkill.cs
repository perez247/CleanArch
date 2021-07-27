using System;
using Domain.BaseClasses;

namespace Domain.IndividualSection
{
    /// <summary>
    /// Skills of an individual
    /// </summary>
    public class IndividualSkill : BaseUpdate
    {
        /// <summary>
        /// Id of this individual skill
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// The User this skill is connected to
        /// </summary>
        /// <value></value>
        public Individual Individual { get; set; }

        /// <summary>
        /// The id of the UserCollection this skill is connected to
        /// </summary>
        /// <value></value>
        public Guid IndividualId { get; set; }

        /// <summary>
        /// The name of the skill
        /// maxlength of 128 characters
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        
        /// <summary>
        /// Level of the skill
        /// </summary>
        /// <value></value>
        public int Level { get; set; }
    }
}