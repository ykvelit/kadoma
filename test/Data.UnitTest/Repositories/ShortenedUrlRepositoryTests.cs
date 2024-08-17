using Data.Repositories;
using Domain.Entities;
using Domain.UnitTest.Fakers;
using Ykvelit.Core.Data;

namespace Data.UnitTest.Repositories;
public class ShortenedUrlRepositoryTests
{
    private readonly ShortenedUrlRepository _subject;

    private readonly Mock<IRepository<ShortenedUrl>> _shortenedUrls = new();

    private readonly ShortenedUrlFaker _faker = new();

    public ShortenedUrlRepositoryTests()
    {
        _subject = new(_shortenedUrls.Object);
    }

    [Fact]
    public async Task CodeExistsAsync_WhenShortenedUrlIsNotNull_ShouldReturnTrue()
    {
        // arrange
        var shortenedUrl = _faker.Generate();

        _shortenedUrls
            .Setup(x => x.FindAsync(It.IsAny<Specification<ShortenedUrl>>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(shortenedUrl);

        // act
        var result = await _subject.CodeExistsAsync("code", CancellationToken.None);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task CodeExistsAsync_WhenShortenedUrlIsNull_ShouldReturnFalse()
    {
        // arrange
        _shortenedUrls
            .Setup(x => x.FindAsync(It.IsAny<Specification<ShortenedUrl>>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ShortenedUrl)null!);

        // act
        var result = await _subject.CodeExistsAsync("code", CancellationToken.None);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldCallFind()
    {
        // arrange
        // act
        await _subject.CodeExistsAsync("code", CancellationToken.None);

        // assert
        _shortenedUrls.Verify(
            x => x.FindAsync(It.IsAny<Specification<ShortenedUrl>>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task InsertAsync_ShouldCallInsert()
    {
        // arrange
        var shortenedUrl = _faker.Generate();

        // act
        await _subject.InsertAsync(shortenedUrl, CancellationToken.None);

        // assert
        _shortenedUrls.Verify(
            x => x.InsertAsync(It.IsAny<ShortenedUrl>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
