using System;
using System.Linq;
using Application.Interfaces.IRepositories.DefaultDataAccess;
using ApplicationValidations;
using Domain.UserSection;
using FluentValidation;

namespace Application.Entities.Authentication.Command.SignUpOrganization
{
    /// <summary>
    /// Validate the signup request
    /// </summary>
    public class SignUpOrganizationValidator : AbstractValidator<SignUpOrganizationCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_defaultDbAuth">Defualt db auth for verifying email and username</param>
        public SignUpOrganizationValidator(IDefaultDataAccessAuthRepository _defaultDbAuth)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Organization Name is required")
                .MaximumLength(200).WithMessage("Maximum of 200 chars");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Min of 6 chars")
                .MaximumLength(128).WithMessage("Max of 128 chars")
                .Must(CommonValidations.BeAValidPassword).WithMessage(CommonValidations.ValidPasswordErrorMessage);

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email");

            RuleFor(x => x.EmailAddress)
                .MustAsync(async (x, email, y) => await _defaultDbAuth.EmailOrUsernameAvailable(x.EmailAddress)).WithMessage(x => $"{x.EmailAddress} has been taken")
                .When(x => !string.IsNullOrEmpty(x.EmailAddress));

            RuleFor(x => x.YearFounded)
                .NotEmpty().WithMessage("Year established is required")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Year must be less than or equal to curent year");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .Matches("^[a-zA-Z0-9._]*$").WithMessage("Only letters, numbers, periods and underscore");

            RuleFor(x => x.Username)
                .MustAsync(async (x, email, y) => await _defaultDbAuth.EmailOrUsernameAvailable(x.Username)).WithMessage(x => $"{x.Username} has been taken")
                .When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.OrganizationType)
                .NotEmpty().WithMessage("Type of organization is required");

            RuleFor(x => x.OrganizationType)
                .Must(x => Enum.GetNames(typeof(OrganizationType)).Contains(x.ToLower())).WithMessage("Ivalid Type of Organization. SME, Charity or NGO");

        }
    }
}