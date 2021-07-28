using System.Text.RegularExpressions;

namespace ApplicationValidations
{
    /// <summary>
    /// Validate common stuffs
    /// </summary>
    public static class CommonValidations
    {
        /// <summary>
        /// Server side to check if the password is valid
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool BeAValidPassword(string password) {

            if(string.IsNullOrEmpty(password))
                return false;

            var validator = new Regex("^.*(?=.{6,50})(?=.*\\d)(?=.*[A-Za-z]).*$");
            return validator.Match(password).Success;
        }

        /// <summary>
        /// Error message to show if the right password is not selected
        /// </summary>
        public static string ValidPasswordErrorMessage = "lowercase, uppercase, number, min 6 and max 50";

    }
}