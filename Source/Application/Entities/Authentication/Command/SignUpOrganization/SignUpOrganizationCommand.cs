using Application.Common.GenericDtos.UserDtoSection;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using Application.Interfaces.IServices;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Entities.Authentication.Command.SignUpOrganization
{
    /// <summary>
    /// Request to signup organization
    /// </summary>
    public class SignUpOrganizationCommand  : IRequest<SignUpUserDto>
    {
        /// <summary>
        /// Name of the organization
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        /// Email address of the organization
        /// </summary>
        /// <value></value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// User name of the organization
        /// </summary>
        /// <value></value>
        public string Username { get; set; }

        /// <summary>
        /// Type of organization
        /// </summary>
        /// <value></value>
        public string OrganizationType { get; set; }

        /// <summary>
        /// Year this organization was founded
        /// </summary>
        /// <value></value>
        public int YearFounded { get; set; }

        /// <summary>
        /// Type of industry
        /// </summary>
        /// <value></value>
        public string Industry { get; set; }

        /// <summary>
        /// Password of the organization
        /// </summary>
        /// <value></value>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class to handle request
    /// </summary>
    public class SignUpOrganizationHandler : IRequestHandler<SignUpOrganizationCommand, SignUpUserDto>
    {
        private IDefaultDataAccessUnitOfWork _defaultDbUnitOfWork;
        private IApplicationEmailService _emailService;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iUnitOfWork">Default database context</param>
        /// <param name="emailService">Email Service</param>
        public SignUpOrganizationHandler(IDefaultDataAccessUnitOfWork iUnitOfWork, IApplicationEmailService emailService)
        {
            _defaultDbUnitOfWork = iUnitOfWork;
            _emailService = emailService;
        }

        /// <summary>
        /// Method invoke to handle request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SignUpUserDto> Handle(SignUpOrganizationCommand request, CancellationToken cancellationToken)
        {
            var result = await _defaultDbUnitOfWork.DefaultDataAccessAuthRepository.SignUpOrganization(request);

            _emailService.SendVerifyEmailLinkAsync(new EmailServiceData() { User = result.User, Token = result.Token });

            return new SignUpUserDto 
            {
                UserId = result.User.Id.ToString(),
                Token = WebUtility.UrlDecode(result.Token)
            };        
        }
    }
}