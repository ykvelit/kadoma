using Application.Queries.GetShortenedUrl;
using FluentValidation.TestHelper;

namespace Application.UnitTest.Queries.GetShortenedUrl;
public class GetShortenedUrlQueryValidatorTests
{
    private readonly GetShortenedUrlQueryValidator _subject;

    public GetShortenedUrlQueryValidatorTests()
    {
        _subject = new();
    }

    [Fact]
    public void WhenCodeIsEmpty_ShouldHaveValidationErrorForCode()
    {
        // arrange
        var code = string.Empty;
        var query = new GetShortenedUrlQuery(code);

        // act
        var result = _subject.TestValidate(query);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Code);
    }
}
