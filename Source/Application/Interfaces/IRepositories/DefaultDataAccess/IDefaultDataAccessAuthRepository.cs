using System.Threading.Tasks;
using Application.Entities.Authentication.Command.SignInUser;
using Application.Entities.Authentication.Command.SignUpIndividual;
using Application.Entities.Authentication.Command.SignUpOrganization;
using Application.Entities.Authentication.Command.VerifyEmailAddress;
using Application.Interfaces.IServices;
using Domain.UserSection;

namespace Application.Interfaces.IRepositories.DefaultDataAccess
{
    /// <summary>
    /// Interface for authentication for default access repository
    /// </summary>
    public interface IDefaultDataAccessAuthRepository
    {
        /// <summary>
        /// Sign up an individual into the application
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<EmailServiceData> SignUpIndividual(SignUpIndividualCommand command);

        /// <summary>
        /// Sign up an organization into the application
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<EmailServiceData> SignUpOrganization(SignUpOrganizationCommand command);

        /// <summary>
        /// Sign in a user to the platform
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<User> SignInUser(SignInUserCommand command);

        /// <summary>
        /// Check if email or usermame has not been taken by another user
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <returns></returns>
        Task<bool> EmailOrUsernameAvailable(string usernameOrEmail);

        /// <summary>
        /// Verifies the email addres of the user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<bool> VerifyEmailAddress(VerifyEmailAddressCommand command);
    }
}