using FluentValidation;

namespace CleanApplication.Application.Tags.Queries.GetTagsAdmin
{
    public class GetTagsQueryValidator : AbstractValidator<GetTagsQuery>
    {
        public GetTagsQueryValidator()
        {
            //RuleFor(x => x.Key)
            //  .NotNull()
            //  .NotEmpty().WithMessage("ListId is required.");
        }
    }
}
