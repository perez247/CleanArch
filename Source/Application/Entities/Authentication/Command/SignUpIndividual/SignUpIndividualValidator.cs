using Application.Interfaces.IRepositories.DefaultDataAccess;
using ApplicationValidations;
using FluentValidation;

namespace Application.Entities.Authentication.Command.SignUpIndividual
{
    /// <summary>
    /// Validate the request
    /// </summary>
    public class SignUpIndividualValidator : AbstractValidator<SignUpIndividualCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_defaultDbAuth">Defualt db auth for verifying email and username</param>
        public SignUpIndividualValidator(IDefaultDataAccessAuthRepository _defaultDbAuth)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(255).WithMessage("Maximum of 255 chars");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(255).WithMessage("Maximum of 255 chars");
                                    
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Minimum of 6 chars")
                .MaximumLength(128).WithMessage("Maximum of 128 chars")
                .Must(CommonValidations.BeAValidPassword).WithMessage(CommonValidations.ValidPasswordErrorMessage);

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email")
                .MaximumLength(255).WithMessage("Maximum of 255 chars");

            RuleFor(x => x.EmailAddress)
                .MustAsync(async (x, email, y) => await _defaultDbAuth.EmailOrUsernameAvailable(x.EmailAddress)).WithMessage(x => $"{x.EmailAddress} has been taken")
                .When(x => !string.IsNullOrEmpty(x.EmailAddress));

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(255).WithMessage("Maximum of 255 chars")
                .Matches("^[a-zA-Z0-9._]*$").WithMessage("Only letters, numbers, periods and underscore");

            RuleFor(x => x.Username)
                .MustAsync(async (x, email, y) => await _defaultDbAuth.EmailOrUsernameAvailable(x.Username)).WithMessage(x => $"{x.Username} has been taken")
                .When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth")
                .Must((x, dob) => TimeValidator.BeAValidDateRange(x.DateOfBirth)).WithMessage("Only between 10 - 100 years");
        }
    }
}
