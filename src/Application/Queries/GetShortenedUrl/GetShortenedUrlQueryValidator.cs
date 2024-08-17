using FluentValidation;

namespace Application.Queries.GetShortenedUrl;

public class GetShortenedUrlQueryValidator : AbstractValidator<GetShortenedUrlQuery>
{
    public GetShortenedUrlQueryValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty();
    }
}
