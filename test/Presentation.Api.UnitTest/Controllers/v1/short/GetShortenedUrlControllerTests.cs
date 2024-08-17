using Application.Queries.GetShortenedUrl;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Controllers.v1.@short;

namespace Presentation.Api.UnitTest.Controllers.v1.@short;

public class GetShortenedUrlControllerTests
{
    private readonly GetShortenedUrlController _subject;

    private readonly Mock<ISender> _sender = new();

    public GetShortenedUrlControllerTests()
    {
        _subject = new(_sender.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnRedirectResult()
    {
        // arrange
        var url = "http://localhost";
        _sender
            .Setup(x => x.Send(It.IsAny<GetShortenedUrlQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(url);

        // act
        var result = await _subject.GetAsync("code", CancellationToken.None);

        // assert
        result.Should().BeOfType<RedirectResult>();
    }
}
