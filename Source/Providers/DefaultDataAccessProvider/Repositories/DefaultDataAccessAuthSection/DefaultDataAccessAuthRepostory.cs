using System.Linq;
using System.Threading.Tasks;
using Application.Entities.Authentication.Command.VerifyEmailAddress;
using Application.Exceptions;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using Application.Interfaces.IServices;
using DefaultDataAccessProvider.Repositories;
using Domain.IndividualSection;
using Domain.OrganizationSection;
using Domain.UserSection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DefaultDatabaseContext.Repositories.DefaultDataAccessAuthSection
{
    /// <summary>
    /// Authentication accessing the data access context primarily
    /// </summary>
    public class DefaultDataAccessAuthRepostory : IDefaultDataAccessAuthRepository
    {
        /// <summary>
        /// Collection of tables(List) as ORM used for the DB
        /// </summary>
        public DefaultDataAccessContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="defaultDataAccessUserManager"></param>
        /// <param name="defaultDataAccessSignInManager"></param>
        public DefaultDataAccessAuthRepostory(
            DefaultDataAccessContext context,
            UserManager<User> defaultDataAccessUserManager,
            SignInManager<User>  defaultDataAccessSignInManager
            )
        {
            _context = context;
            _userManager = defaultDataAccessUserManager;
            _signInManager = defaultDataAccessSignInManager;
        }

        /// <summary>
        /// Generate token used for verifying the email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<EmailServiceData> GenerateEmailVerificationToken(User user) {
            // Generate token to be used for email verification
            var Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // return data to be used for email service
            return new EmailServiceData { User = user, Token = Token };
        }

        /// <summary>
        /// Generate token used for resseting password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<EmailServiceData> GenerateForgotPasswordToken(User user) {
            // Generate token to be used for passsword reset
            var Token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // return data to be used for email service
            return new EmailServiceData { User = user, Token = Token };
        }

        /// <summary>
        /// Check if email or usermame has not been taken by another user
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <returns></returns>
        public async Task<bool> EmailOrUsernameAvailable(string usernameOrEmail)
        {
            if (string.IsNullOrEmpty(usernameOrEmail))
                throw new CustomMessageException("Cannot lookup email, please try again later");

            var user = await _userManager.Users.SingleOrDefaultAsync(
                            u => u.NormalizedEmail == usernameOrEmail.ToUpper() || u.NormalizedUserName == usernameOrEmail.ToUpper());

            return user == null;
        }

        /// <summary>
        /// Sign up an individual into the application
        /// </summary>
        /// <param name="NewIndividual"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<Individual> SignUpIndividual(Individual NewIndividual, string Password) {

            IdentityResult result;
            // Try saving the user
            try
            {
                result = await _userManager.CreateAsync(NewIndividual, Password);
            }
            catch (System.Exception e)
            {
                throw new CustomMessageException(e.Message);
            }

            // If creating of user did not complete throw error
            if (!result.Succeeded)
                throw new CustomMessageException(result.Errors.First().Description);

            return NewIndividual;
        }

        /// <summary>
        /// Sign up an organization into the application
        /// </summary>
        /// <param name="NewOrganization"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<Organization> SignUpOrganization(Organization NewOrganization, string Password) {

            IdentityResult result;
            // Try saving the user
            try
            {
                result = await _userManager.CreateAsync(NewOrganization, Password);
            }
            catch (System.Exception e)
            {
                throw new CustomMessageException(e.Message);
            }

            // If creating of user did not complete throw error
            if (!result.Succeeded)
                throw new CustomMessageException(result.Errors.First().Description);

            return NewOrganization;
        }

        /// <summary>
        /// Resend email verification token used to verify an email
        /// </summary>
        /// <param name="EmailAddressOrUserName"></param>
        /// <returns></returns>
        public async Task<EmailServiceData> ResendEmailVerificationToken(string EmailAddressOrUserName) {

            var userInDb = await _context.Users.Include(x => x.AdditionalDetail)
                                                        .FirstOrDefaultAsync( u => 
                                                            u.UserName.ToLower() == EmailAddressOrUserName.ToLower() ||
                                                            u.Email.ToLower() == EmailAddressOrUserName.ToLower());

            if (userInDb == null)
                throw new CustomMessageException($"{EmailAddressOrUserName} is not on the ECO platform");

            if (userInDb.EmailConfirmed)
                throw new CustomMessageException($"{EmailAddressOrUserName} has already been confirmed");

            // Generate token to be used for email verification
            return await GenerateEmailVerificationToken(userInDb);
        }  

        /// <summary>
        /// Check password and lock if failed
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<bool> CheckPasswordAndLockOn5FailedAttempts(User User, string Password)
        {
            if (!await _userManager.CheckPasswordAsync(User, Password))
            {
                // Increase failed attemptes
                await _userManager.AccessFailedAsync(User);

                // Throw invalid login agian
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifies the email addres of the user
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<bool> VerifyEmailAddress(User User, string Token)
        {
            var result = await _userManager.ConfirmEmailAsync(User, Token);

            return result.Succeeded;
        }

        /// <summary>
        /// Change user password with token
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Token"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordWithToken(User User, string Token, string NewPassword)
        {
            var result = await _userManager.ResetPasswordAsync(User, Token, NewPassword);

            return result.Succeeded;
        }
    }
}