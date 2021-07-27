using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.RequestResponsePipeline
{
    /// <summary>
    /// Catches all the validation errors
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TResponse> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validators"></param>
        /// <param name="logger"></param>
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<TResponse> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        /// <summary>
        /// Handle method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogWarning(next.Target.ToString());

            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new AppFluentValidationException(failures);
            }
            else
            {

                return next();
            }
        }
    }
}