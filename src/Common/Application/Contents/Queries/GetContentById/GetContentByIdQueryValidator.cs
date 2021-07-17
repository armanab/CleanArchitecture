using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Queries.GetContentById
{
    public class GetContentByIdQueryValidator : AbstractValidator<GetContentByIdQuery>
    {
        public GetContentByIdQueryValidator()
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty().WithMessage("شناسه خالی است");
        }
    }
}
