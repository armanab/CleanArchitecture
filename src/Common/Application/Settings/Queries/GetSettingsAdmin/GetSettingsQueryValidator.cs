using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Settings.Queries.GetSettingsAdmin
{
    public class GetSettingsQueryValidator : AbstractValidator<GetSettingsQuery>
    {
        public GetSettingsQueryValidator()
        {
            //RuleFor(x => x.Key)
            //  .NotNull()
            //  .NotEmpty().WithMessage("ListId is required.");
        }
    }
}
