namespace Application.Entities.Authentication.Command.VerifyEmailAddress
{
    /// <summary>
    /// Response to request to verify email address
    /// </summary>
    public class VerifyemailAddressDto
    {
        /// <summary>
        /// If the verification was successful or not
        /// </summary>
        /// <value></value>
        public bool Success { get; set; }
    }
}