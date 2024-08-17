using Application.Queries.GetShortenedUrl;
using Domain.Repositories;
using Domain.UnitTest.Fakers;

namespace Application.UnitTest.Queries.GetShortenedUrl;

public class GetShortenedUrlQueryHandlerTests
{
    private readonly GetShortenedUrlQueryHandler _subject;

    private readonly Mock<IShortenedUrlRepository> _shortenedUrls = new();

    private readonly ShortenedUrlFaker _faker = new();

    public GetShortenedUrlQueryHandlerTests()
    {
        _subject = new(_shortenedUrls.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnLongUrl()
    {
        // arrange
        var code = "code";
        var query = new GetShortenedUrlQuery(code);

        var shortenedUrl = _faker.Generate();
        _shortenedUrls
            .Setup(x => x.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(shortenedUrl);

        // act
        var result = await _subject.Handle(query, CancellationToken.None);

        // assert
        result.Should().Be(shortenedUrl.LongUrl);
    }
}
