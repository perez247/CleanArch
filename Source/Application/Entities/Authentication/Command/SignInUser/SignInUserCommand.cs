using System.Threading;
using System.Threading.Tasks;
using Application.Common.ApplicationHelperFunctions;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using MediatR;

namespace Application.Entities.Authentication.Command.SignInUser
{
    /// <summary>
    /// Request to sign in user
    /// </summary>
    public class SignInUserCommand : IRequest<SignInUserDto>
    {
        /// <summary>
        /// Email address of the user
        /// </summary>
        /// <value></value>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// Password of the user
        /// </summary>
        /// <value></value>
        public string Password { get; set; }
    }

    /// <summary>
    /// Class to handle request
    /// </summary>
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, SignInUserDto>
    {
        private readonly IDefaultDataAccessUnitOfWork _unitOfWork;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SignInUserHandler(IDefaultDataAccessUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method called to handle the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SignInUserDto> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.DefaultDataAccessAuthRepository.SignInUser(request);
            
            var token = TokenHelperFunctions.generateUserToken(result);
            return SignInUserDto.Generate(result, token);
        }
    }
}