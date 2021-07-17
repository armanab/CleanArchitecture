using CleanApplication.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Commands.Create
{
    public class CreateSettingCommandValidator : AbstractValidator<CreateSettingCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateSettingCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
       .MaximumLength(2048).WithMessage("نام کلید نباید بیشتر از 2048 کاراکتر باشد.")
       .NotNull().NotEmpty().WithMessage("نام کلید نباید خالی باشد")
             .MustAsync(BeUniqueName).WithMessage("عنوان مشخص شده از قبل وجود دارد.");
        }
        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Settings
                .AllAsync(l => l.Name != name);
        }

    }
}
