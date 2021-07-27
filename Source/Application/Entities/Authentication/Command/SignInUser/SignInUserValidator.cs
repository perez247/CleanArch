using FluentValidation;

namespace Application.Entities.Authentication.Command.SignInUser
{
    /// <summary>
    /// Validates the request
    /// </summary>
    public class SignInUserValidator : AbstractValidator<SignInUserCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SignInUserValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}