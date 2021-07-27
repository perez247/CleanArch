using Application.Common.CustomAnnotations;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Entities.Authentication.Command.VerifyEmailAddress
{
    /// <summary>
    /// Verify user email address
    /// </summary>
    public class VerifyEmailAddressCommand : IRequest<VerifyemailAddressDto>
    {
        /// <summary>
        /// Id of the user to verify the email address
        /// </summary>
        /// <value></value>
        [VerifyGuidAnnotation]
        public string UserId { get; set; }

        /// <summary>
        /// Token to use for verification
        /// </summary>
        /// <value></value>
        public string Token { get; set; }
    }

    /// <summary>
    /// Class to handle request
    /// </summary>
    public class VerifyEmailAddressHandler : IRequestHandler<VerifyEmailAddressCommand, VerifyemailAddressDto>
    {
        private IDefaultDataAccessUnitOfWork _defaultDbUnitOfWork;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iUnitOfWork">Default database context</param>
        public VerifyEmailAddressHandler(IDefaultDataAccessUnitOfWork iUnitOfWork)
        {
            _defaultDbUnitOfWork = iUnitOfWork;
        }

        /// <summary>
        /// Method invoke to handle request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<VerifyemailAddressDto> Handle(VerifyEmailAddressCommand request, CancellationToken cancellationToken)
        {
            request.Token = WebUtility.UrlDecode(request.Token);
            var result = await _defaultDbUnitOfWork.DefaultDataAccessAuthRepository.VerifyEmailAddress(request);

            return new VerifyemailAddressDto { Success = result };
        }
    }
}