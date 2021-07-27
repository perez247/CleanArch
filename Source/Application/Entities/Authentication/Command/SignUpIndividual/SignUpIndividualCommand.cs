using Application.Common.GenericDtos.UserDtoSection;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using Application.Interfaces.IServices;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Entities.Authentication.Command.SignUpIndividual
{
    /// <summary>
    /// Command to signup a user to the platform
    /// </summary>
    public class SignUpIndividualCommand : IRequest<SignUpUserDto>
    {
        /// <summary>
        /// First name of the user
        /// </summary>
        /// <value>string</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        /// <value>string</value>
        public string LastName { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        /// <value>string</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Username of the user
        /// </summary>
        /// <value>string</value>
        public string Username { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        /// <value>string</value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Choosen password of the user
        /// </summary>
        /// <value>string</value>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class to handle request
    /// </summary>
    public class SignUpUserHandler : IRequestHandler<SignUpIndividualCommand, SignUpUserDto>
    {
        private IDefaultDataAccessUnitOfWork _defaultDbUnitOfWork;
        private IApplicationEmailService _emailService;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iUnitOfWork">Default database context</param>
        /// <param name="emailService">Email Service</param>
        public SignUpUserHandler(IDefaultDataAccessUnitOfWork iUnitOfWork, IApplicationEmailService emailService)
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
        public async Task<SignUpUserDto> Handle(SignUpIndividualCommand request, CancellationToken cancellationToken)
        {
            var result = await _defaultDbUnitOfWork.DefaultDataAccessAuthRepository.SignUpIndividual(request);

            _emailService.SendVerifyEmailLinkAsync(new EmailServiceData() { User = result.User, Token = result.Token });

            return new SignUpUserDto 
            {
                UserId = result.User.Id.ToString(),
                Token = WebUtility.UrlDecode(result.Token)
            };
        }
    }
}
