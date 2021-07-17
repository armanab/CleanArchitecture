using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Queries.GetSettingById
{
    public class GetSettingByNameQueryValidator : AbstractValidator<GetSettingByNameQuery>
    {
        public GetSettingByNameQueryValidator()
        {
            RuleFor(x => x.Name)
              .NotNull()
              .NotEmpty().WithMessage("کلید خالی است");
        }
    }
}
