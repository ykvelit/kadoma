using Domain.Repositories;
using Domain.Services;

namespace Domain.UnitTest.Services;

public class ShortenedUrlUniqueCodeGeneratorTests
{
    private readonly ShortenedUrlUniqueCodeGenerator _subject;

    private readonly Mock<IShortenedUrlRepository> _shortenedUrls = new();
    private readonly Mock<IUniqueCodeGenerator> _uniqueCode = new();

    public ShortenedUrlUniqueCodeGeneratorTests()
    {
        _subject = new(_shortenedUrls.Object, _uniqueCode.Object);
    }

    [Fact]
    public async Task GenerateAsync_WhenCodeIsUnique_ShouldReturn()
    {
        // arrange
        var code = "code";
        _uniqueCode
            .Setup(x => x.Generate(It.IsAny<int>()))
            .Returns(code);

        _shortenedUrls
            .Setup(x => x.CodeExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // act
        var result = await _subject.GenerateAsync(CancellationToken.None);

        // assert
        result.Should().Be(code);
    }

    [Fact]
    public async Task GenerateAsync_WhenCodeIsNotUnique_ShouldReturnNextCode()
    {
        // arrange
        var code1 = "code1";
        var code2 = "code2";
        _uniqueCode
            .SetupSequence(x => x.Generate(It.IsAny<int>()))
            .Returns(code1)
            .Returns(code2);

        _shortenedUrls
            .SetupSequence(x => x.CodeExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);

        // act
        var result = await _subject.GenerateAsync(CancellationToken.None);

        // assert
        result.Should().Be(code2);
    }
}
