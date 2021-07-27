namespace Application.Common.GenericDtos.UserDtoSection
{
    /// <summary>
    /// to be used as a return type in development when signing up a user
    /// Individual, organization, staff
    /// </summary>
    public class SignUpUserDto
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        /// <value></value>
        public string UserId { get; set; }

        /// <summary>
        /// Token of the user
        /// </summary>
        /// <value></value>
        public string Token { get; set; }
    }
}