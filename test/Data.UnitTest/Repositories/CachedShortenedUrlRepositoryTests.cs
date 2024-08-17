using Data.Repositories;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnitTest.Fakers;
using Ykvelit.Core.Caching;

namespace Data.UnitTest.Repositories;

public class CachedShortenedUrlRepositoryTests
{
    private readonly CachedShortenedUrlRepository _subject;

    private readonly Mock<IShortenedUrlRepository> _inner = new();
    private readonly Mock<ICache> _cache = new();

    private readonly ShortenedUrlFaker _faker = new();

    public CachedShortenedUrlRepositoryTests()
    {
        _subject = new CachedShortenedUrlRepository(_inner.Object, _cache.Object);
    }

    [Fact]
    public async Task CodeExistsAsync_WhenCachedValueIsNotNull_ShouldReturnFalseAndNotCallInnerCodeExist()
    {
        // arrange
        var shortenedUrl = _faker.Generate();
        _cache
            .Setup(x => x.GetAsync<ShortenedUrl>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(shortenedUrl);

        // act
        var result = await _subject.CodeExistsAsync("code", CancellationToken.None);

        // assert
        result.Should().BeFalse();
        _inner.Verify(
            x => x.CodeExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact]
    public async Task CodeExistsAsync_WhenCachedValueIsNull_ShouldCallInnerCodeExist()
    {
        // arrange
        _cache
            .Setup(x => x.GetAsync<ShortenedUrl>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ShortenedUrl)null!);

        // act
        await _subject.CodeExistsAsync("code", CancellationToken.None);

        // assert
        _inner.Verify(
            x => x.CodeExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task GetByCodeAsync_WhenCachedValueIsNotNull_ShouldNotCallInnerGetByCode()
    {
        // arrange
        var shortenedUrl = _faker.Generate();
        _cache
            .Setup(x => x.GetAsync<ShortenedUrl>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(shortenedUrl);

        // act
        await _subject.GetByCodeAsync("code", CancellationToken.None);

        // assert
        _inner.Verify(
            x => x.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never
        );
    }

    [Fact]
    public async Task GetByCodeAsync_WhenCachedValueIsNull_ShouldCallInnerGetByCodeAndCacheSet()
    {
        // arrange
        _cache
            .Setup(x => x.GetAsync<ShortenedUrl>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ShortenedUrl)null!);

        // act
        await _subject.GetByCodeAsync("code", CancellationToken.None);

        // assert
        _inner.Verify(
            x => x.GetByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
        _cache.Verify(
            x => x.SetAsync(It.IsAny<string>(), It.IsAny<ShortenedUrl>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact]
    public async Task InsertAsync_ShouldCallInnerInsert()
    {
        // arrange
        var shortenedUrl = _faker.Generate();

        // act
        await _subject.InsertAsync(shortenedUrl, CancellationToken.None);

        // assert
        _inner.Verify(
            x => x.InsertAsync(It.IsAny<ShortenedUrl>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}