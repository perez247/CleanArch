using System;

namespace ApplicationValidations
{
    /// <summary>
    /// Validator for Time stuffs
    /// </summary>
    public static class TimeValidator
    {
        /// <summary>
        /// Check password
        /// </summary>
        public static string ValidPasswordErrorMessage = "Min 5 chars with one lowercase, uppercase, special char, number";

        /// <summary>
        /// Must be a date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        /// <summary>
        /// User of this application ,ust be between the ages of 10 and 100 years
        /// </summary>
        /// <param name="date"></param>
        /// <param name="MinAge"></param>
        /// <param name="MaxAge"></param>
        /// <returns></returns>
        public static bool BeAValidDateRange(DateTime date, int MinAge = 10, int MaxAge = 100)
        {
            if (!TimeValidator.BeAValidDate(date))
                return true;

            return DateTime.Now.AddYears(-MaxAge) <= date && DateTime.Now.AddYears(-MinAge) >= date;
        }

        /// <summary>
        /// Date must not be today
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool NotNow(DateTime date)
        {
            return DateTime.Now >= date;
        }

        /// <summary>
        /// End date cannot be greater than 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static bool EndDateGreaterThanStartDate(DateTime startDate, DateTime endDate)
        {
            return endDate > startDate;
        }

    }
}