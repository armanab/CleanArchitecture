using FluentValidation;

namespace CleanApplication.Application.Users.Queries.GetToken
{
    public class CreateUserCommandValidator : AbstractValidator<GetTokenCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.Email)
                .MaximumLength(100).WithMessage("ایمیل نباید از 100 کاراکتر بیشتر باشد.")
                .NotEmpty().WithMessage("ایمیل خالی است.");
            RuleFor(v => v.Email)
                .EmailAddress().WithMessage("قالب ایمیل نادرست است. ");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("کلمه عبور خالی است.");
        }
    }
}
