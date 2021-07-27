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

            var validator = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{6,}$");
            return validator.Match(password).Success;
        }

        /// <summary>
        /// Error message to show if the right password is not selected
        /// </summary>
        public static string ValidPasswordErrorMessage = "lowercase, uppercase, number and min of 6 chars";

    }
}