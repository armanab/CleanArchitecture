using CleanApplication.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Tags.Commands.Create
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(p => p.KeyName).NotNull().NotEmpty().WithMessage("کلید نباید خالی باشد.");
            RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("نام نباید خالی باشد.");
        }
      
    }
}
