using Application.Commands.ShortenUrl;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Application.UnitTest.Commands.ShortenUrl;

public class ShortenUrlCommandHandlerTests
{
    private readonly ShortenUrlCommandHandler _subject;

    private readonly Mock<IShortenedUrlUniqueCodeGenerator> _uniqueCode = new();
    private readonly Mock<IShortenedUrlRepository> _shortenedUrls = new();

    public ShortenUrlCommandHandlerTests()
    {
        _subject = new(_uniqueCode.Object, _shortenedUrls.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallInsert()
    {
        // arrange
        var code = "code";
        _uniqueCode
            .Setup(x => x.GenerateAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(code);

        var url = "http://localhost";
        var command = new ShortenUrlCommand(url);

        // act
        await _subject.Handle(command, CancellationToken.None);

        // assert
        _shortenedUrls.Verify(
            x => x.InsertAsync(It.IsAny<ShortenedUrl>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
