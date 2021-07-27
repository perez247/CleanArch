using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities.Authentication.Command.SignInUser;
using Application.Entities.Authentication.Command.SignUpIndividual;
using Application.Entities.Authentication.Command.SignUpOrganization;
using Application.Entities.Authentication.Command.VerifyEmailAddress;
using Application.Exceptions;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using Application.Interfaces.IServices;
using DefaultDataAccessProvider.Repositories;
using Domain.IndividualSection;
using Domain.OrganizationSection;
using Domain.UserSection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<EmailServiceData> SignUpIndividual(SignUpIndividualCommand command) {
            
            // Instantiate a new Individuak
            var newIndividual = new Individual(command.EmailAddress)
            {
                Email = command.EmailAddress.ToLower(),
                UserName = command.Username,
                AgreeToTermsAndCondition = true,
                Deleted = false,
                Activated = true,
                AdditionalDetail = new UserAdditionalDetail()
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    DateOfBirth = command.DateOfBirth
                }
            };

            IdentityResult result;
            // Try saving the user
            try
            {
                result = await _userManager.CreateAsync(newIndividual, command.Password);
            }
            catch (System.Exception e)
            {
                throw new CustomMessageException(e.Message);
            }

            // If creating of user did not complete throw error
            if (!result.Succeeded)
                throw new CustomMessageException(result.Errors.First().Description);

            // Generate token to be used for email verification
            var Token = await _userManager.GenerateEmailConfirmationTokenAsync(newIndividual);

            // var user = _context.Ro
            // return data to be used for email service
            return new EmailServiceData { User = newIndividual, Token = Token };
        }

        /// <summary>
        /// Sign up an organization into the application
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<EmailServiceData> SignUpOrganization(SignUpOrganizationCommand command) {
            
            // Instantiate a new organization
            var newOrganization = new Organization(command.EmailAddress)
            {
                Email = command.EmailAddress.ToLower(),
                UserName = command.Username,
                AgreeToTermsAndCondition = true,
                Deleted = false,
                Activated = true,
                AdditionalDetail = new UserAdditionalDetail()
                {
                    OrganizationName = command.Name,
                    OrganizationType = (OrganizationType)Enum.Parse(typeof(OrganizationType), command.OrganizationType, true),
                    YearEstablished = command.YearFounded
                }
            };

            IdentityResult result;
            // Try saving the user
            try
            {
                result = await _userManager.CreateAsync(newOrganization, command.Password);
            }
            catch (System.Exception e)
            {
                throw new CustomMessageException(e.Message);
            }

            // If creating of user did not complete throw error
            if (!result.Succeeded)
                throw new CustomMessageException(result.Errors.First().Description);

            // Generate token to be used for email verification
            var Token = await _userManager.GenerateEmailConfirmationTokenAsync(newOrganization);

            // var user = _context.Ro
            // return data to be used for email service
            return new EmailServiceData { User = newOrganization, Token = Token };
        }

        /// <summary>
        /// Sign in a user to the platform
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<User> SignInUser(SignInUserCommand command)
        {
            var user = await _context.Users.Include(x => x.AdditionalDetail)
                            //  .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                             .FirstOrDefaultAsync(x => 
                                    x.Email.ToLower().Equals(command.EmailAddress.ToLower()) ||
                                    x.UserName.ToLower().Equals(command.EmailAddress.ToLower()) 
                                    );

            if (user == null)
                throw new CustomMessageException("Invalid login credentials");

            // If user is currently locked out let them know
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value.UtcDateTime.ToUniversalTime() > DateTime.Now.ToUniversalTime())
                throw new CustomMessageException($"This account has been locked for now");

            if (!await _userManager.CheckPasswordAsync(user, command.Password))
            {
                // Increase failed attemptes
                await _userManager.AccessFailedAsync(user);

                // Throw invalid login agian
                throw new CustomMessageException("Invalid login credentials");
            }
            
            // If email is not confirmed then throw error, user must verify email first
            if (!user.EmailConfirmed)
                throw new ConfirmEmailException(user.Email);
                
            return user;
            
        }

        /// <summary>
        /// Verifies the email addres of the user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> VerifyEmailAddress(VerifyEmailAddressCommand command)
        {
            var user = await _userManager.Users
                                            .Include(u => u.AdditionalDetail)
                                            .SingleOrDefaultAsync(u => u.Id.ToString() == command.UserId);

            if (user == null)
                return false;

            if (await _userManager.IsEmailConfirmedAsync(user))
                throw new CustomMessageException($"{user.Email} has already been verified");

            var result = await _userManager.ConfirmEmailAsync(user, command.Token);

            return result.Succeeded;
        }
    }
}