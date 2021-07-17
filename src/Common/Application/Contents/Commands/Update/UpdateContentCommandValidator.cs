using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Commands.Update
{
    public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
    {
        public UpdateContentCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200).WithMessage("نام نباید بیشتر از 200 کاراکتر باشد.")
                .NotNull().NotEmpty().WithMessage("نام نباید خالی باشد");

            RuleFor(v => v.Id).NotNull();
        }
    }
}
