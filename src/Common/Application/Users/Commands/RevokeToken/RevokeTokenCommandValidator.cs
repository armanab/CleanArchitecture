using FluentValidation;

namespace CleanApplication.Application.Users.Commands.RevokeToken
{
    public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(v => v.Token)
             .NotEmpty().WithMessage("توکن خالی است.");
        }
    }

}
