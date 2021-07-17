using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApplication.Application.Contents.Queries.GetContentsAdmin
{
    public class GetContentsQueryValidator : AbstractValidator<GetContentsQuery>
    {
        public GetContentsQueryValidator()
        {
            //RuleFor(x => x.)
            //  .NotNull()
            //  .NotEmpty().WithMessage("ListId is required.");
        }
    }
}
