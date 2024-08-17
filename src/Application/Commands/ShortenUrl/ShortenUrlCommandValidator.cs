using FluentValidation;

namespace Application.Commands.ShortenUrl;

public class ShortenUrlCommandValidator : AbstractValidator<ShortenUrlCommand>
{
    public ShortenUrlCommandValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty()
            .Must(x => Uri.TryCreate(x, UriKind.Absolute, out _))
            ;
    }
}
