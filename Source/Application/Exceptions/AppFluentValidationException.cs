using Application.Interfaces.IExceptions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    /// <summary>
    /// Fluent validation exception
    /// </summary>
    public class AppFluentValidationException : Exception
    {
        /// <summary>
        /// COnstructor
        /// </summary>
        public AppFluentValidationException()
            : base("One or more")
        {
            Failures = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor with parameter
        /// </summary>
        /// <param name="failures"></param>
        public AppFluentValidationException(IList<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// List of errors
        /// </summary>
        /// <value></value>
        public IDictionary<string, string[]> Failures { get; }
    }
}
