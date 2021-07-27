using System;
using Domain.UserSection;

namespace Domain.MediaSection
{
    /// <summary>
    /// Media associated to a user
    /// </summary>
    public class UserMedia
    {
        /// <summary>
        /// Id of this user media
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the user this media is connected to
        /// </summary>
        /// <value></value>
        public Guid UserId { get; set; }

        /// <summary>
        /// User this media is connected to
        /// </summary>
        /// <value></value>
        public User User { get; set; }

        /// <summary>
        /// Background picture of this user
        /// </summary>
        /// <value></value>
        public BackgroundPicture BackgroundPicture { get; set; }

        /// <summary>
        /// Profile picture of this user
        /// </summary>
        /// <value></value>
        public ProfilePicture ProfilePicture { get; set; }
    }
}