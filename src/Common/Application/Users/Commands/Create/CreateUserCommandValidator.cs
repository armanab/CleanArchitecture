using FluentValidation;
using System;

namespace CleanApplication.Application.Users.Commands.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Email)
                .MaximumLength(100).WithMessage("ایمیل نباید از 100 کاراکتر بیشتر باشد.")
                .NotEmpty().WithMessage("ایمیل خالی است.");
            RuleFor(v => v.Email)
                .EmailAddress().WithMessage("قالب ایمیل نادرست است.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("کلمه عبور نباید خالی باشد.");

            RuleFor(v => v.SmsCode)
                .NotEmpty().WithMessage("کد تایید نباید خالی باشد.");

            RuleFor(v => v.SmsCode)
                .Matches(@"^\d{5}$")
                .WithMessage("کد تایید معتبر نمی باشد.");


            RuleFor(p => p.DateOfBirth)
            .Must(BeAValidAge).WithMessage("نا معتبر {PropertyName}");

        }
        protected bool BeAValidAge(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }

            return false;
        }

    }
}
