using FluentValidation;

namespace Application.Entities.Authentication.Command.VerifyEmailAddress
{
    /// <summary>
    /// Class to validate 
    /// </summary>
    public class VerifyEmailAddressValidator : AbstractValidator<VerifyEmailAddressCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VerifyEmailAddressValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required")
                .MaximumLength(1000).WithMessage("Maximum of 1000 chars");
        }
    }
}