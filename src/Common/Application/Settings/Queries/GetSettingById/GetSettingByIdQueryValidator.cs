using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Queries.GetSettingById
{
    public class GetSettingByIdQueryValidator : AbstractValidator<GetSettingByIdQuery>
    {
        public GetSettingByIdQueryValidator()
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty().WithMessage("شناسه خالی است");
        }
    }
}
