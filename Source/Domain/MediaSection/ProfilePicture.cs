using System;
using Domain.FileSection;

namespace Domain.MediaSection
{
    /// <summary>
    /// Profile picture
    /// </summary>
    public class ProfilePicture : AppFile
    {
        /// <summary>
        /// User this profile is connected to
        /// </summary>
        /// <value></value>
        public UserMedia UserMedia { get; set; }

        /// <summary>
        /// Id of the User this profile is connected to
        /// </summary>
        /// <value></value>
        public Guid? UserMediaId { get; set; }
    }
}