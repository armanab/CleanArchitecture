using CleanApplication.Application.Common.Interfaces;
using FluentValidation;

namespace CleanApplication.Application.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateUserCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            //RuleFor(x => x.Name)
            //    .MaximumLength(2048).WithMessage("نام کلید نباید بیشتر از 2048 کاراکتر باشد.")
            //    .NotNull().NotEmpty().WithMessage("نام کلید نباید خالی باشد")
            //     .MustAsync(BeUniqueName).WithMessage("عنوان مشخص شده از قبل وجود دارد.");

            RuleFor(v => v.Id).NotNull().WithMessage("شناسه خالی است.");
        }
      
    }
}
