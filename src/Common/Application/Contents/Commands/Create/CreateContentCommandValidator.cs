using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Commands.Create
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        public CreateContentCommandValidator()
        {
            RuleFor(x=>x.Name)
                .MaximumLength(200).WithMessage("نام نباید بیشتر از 200 کاراکتر باشد.")
                .NotNull().NotEmpty().WithMessage("نام نباید خالی باشد");
            
        }
    }
}
