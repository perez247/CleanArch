using System.Threading.Tasks;
using Application.Entities.Authentication.Command.VerifyEmailAddress;
using Application.Interfaces.IServices;
using Domain.IndividualSection;
using Domain.OrganizationSection;
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
        /// <param name="NewIndividual"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<Individual> SignUpIndividual(Individual NewIndividual, string Password);

        /// <summary>
        /// Sign up an organization into the application
        /// </summary>
        /// <param name="NewOrganization"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<Organization> SignUpOrganization(Organization NewOrganization, string Password);

        /// <summary>
        /// Check password and lock if failed
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<bool> CheckPasswordAndLockOn5FailedAttempts(User User, string Password);

        /// <summary>
        /// Check if email or usermame has not been taken by another user
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <returns></returns>
        Task<bool> EmailOrUsernameAvailable(string usernameOrEmail);

        /// <summary>
        /// Verifies the email addres of the user
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        Task<bool> VerifyEmailAddress(User User, string Token);

        /// <summary>
        /// Generate token used for verifying the email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<EmailServiceData> GenerateEmailVerificationToken(User user);

        /// <summary>
        /// Generate token used for resseting password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<EmailServiceData> GenerateForgotPasswordToken(User user);

        /// <summary>
        /// Change user password with token
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Token"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordWithToken(User User, string Token, string NewPassword);
    }
}