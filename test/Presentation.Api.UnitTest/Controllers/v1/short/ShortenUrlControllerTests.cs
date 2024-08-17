using Application.Commands.ShortenUrl;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Controllers.v1.@short;

namespace Presentation.Api.UnitTest.Controllers.v1.@short;

public class ShortenUrlControllerTests
{
    private readonly ShortenUrlController _subject;

    private readonly Mock<ISender> _sender = new();

    public ShortenUrlControllerTests()
    {
        var context = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext()
        };

        _subject = new(_sender.Object)
        {
            ControllerContext = context
        };
    }

    [Fact]
    public async Task PostAsync_ShouldReturnOkObjectResult()
    {
        // arrange
        var code = "code";
        _sender
            .Setup(x => x.Send(It.IsAny<ShortenUrlCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(code);

        // act
        var result = await _subject.PostAsync("http://localhost", CancellationToken.None);

        // assert
        result.Should().BeOfType<OkObjectResult>();
    }
}
