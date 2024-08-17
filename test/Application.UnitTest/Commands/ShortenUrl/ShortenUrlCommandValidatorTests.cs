using Application.Commands.ShortenUrl;
using FluentValidation.TestHelper;

namespace Application.UnitTest.Commands.ShortenUrl;
public class ShortenUrlCommandValidatorTests
{
    private readonly ShortenUrlCommandValidator _subject;

    public ShortenUrlCommandValidatorTests()
    {
        _subject = new();
    }

    [Fact]
    public void WhenUrlIsEmpty_ShouldHaveValidationErrorForUrl()
    {
        // arrange
        var url = string.Empty;
        var command = new ShortenUrlCommand(url);

        // act
        var result = _subject.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Url);
    }

    [Fact]
    public void WhenUrlIsNotAValidUri_ShouldHaveValidationErrorForUrl()
    {
        // arrange
        var url = "localhost";
        var command = new ShortenUrlCommand(url);

        // act
        var result = _subject.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Url);
    }
}
