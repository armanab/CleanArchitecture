using FluentValidation;

namespace CleanApplication.Application.Users.Commands.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(v => v.Email)
              .MaximumLength(100).WithMessage("ایمیل نباید از 100 کاراکتر بیشتر باشد.")
              .NotEmpty().WithMessage("ایمیل خالی است.");
            RuleFor(v => v.Email)
                .EmailAddress().WithMessage("قالب ایمیل نادرست است.");
            RuleFor(v => v.PhoneNumber)
              .MaximumLength(11)
              .NotEmpty();

            RuleFor(v => v.Password)
              .NotEmpty().WithMessage("کلمه عبور نباید خالی باشد.");

            RuleFor(v => v.SmsCode)
           .NotEmpty().WithMessage("کد تایید نباید خالی باشد.");

            RuleFor(v => v.SmsCode)
                .Matches(@"^\d{5}$")
                .WithMessage("کد تایید معتبر نمی باشد.");


        }

    }
}
