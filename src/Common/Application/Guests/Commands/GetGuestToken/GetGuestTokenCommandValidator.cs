using FluentValidation;

namespace CleanApplication.Application.Guests.Commands.GetGuestToken
{
    public class GetGuestTokenCommandValidator : AbstractValidator<GetGuestTokenCommand>
    {
        public GetGuestTokenCommandValidator()
        {
            RuleFor(v => v.PhoneNumber)
            .MaximumLength(11)
            .NotEmpty();

            RuleFor(v => v.FirstName)
             .NotEmpty().WithMessage("نام نباید خالی باشد.");


            RuleFor(v => v.LastName)
             .NotEmpty().WithMessage("نام خانوادگی نباید خالی باشد.");



            RuleFor(v => v.SmsCode)
             .NotEmpty().WithMessage("کد تایید نباید خالی باشد.");

            RuleFor(v => v.SmsCode)
                .Matches(@"^\d{5}$")
                .WithMessage("کد تایید معتبر نمی باشد.");
        }
    }
}
