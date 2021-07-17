using FluentValidation;

namespace CleanApplication.Application.Tags.Commands.Update
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
  
        public UpdateTagCommandValidator()
        {
            RuleFor(p => p.KeyName).NotNull().NotEmpty().WithMessage("کلید نباید خالی باشد.");
            RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("نام نباید خالی باشد.");
        }

      

    }
}
