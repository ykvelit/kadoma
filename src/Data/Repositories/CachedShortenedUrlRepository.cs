using Domain.Entities;
using Domain.Repositories;
using Ykvelit.Core.Caching;

namespace Data.Repositories;

public class CachedShortenedUrlRepository(IShortenedUrlRepository inner, ICache cache) : IShortenedUrlRepository
{
    public async Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken)
    {
        var cached = await cache.GetAsync<ShortenedUrl>(code, cancellationToken);

        if (cached is not null)
        {
            return false;
        }

        return await inner.CodeExistsAsync(code, cancellationToken);
    }

    public async Task<ShortenedUrl> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var cached = await cache.GetAsync<ShortenedUrl>(code, cancellationToken);

        if (cached is not null)
        {
            return cached;
        }

        var value = await inner.GetByCodeAsync(code, cancellationToken);
        await cache.SetAsync(code, value, cancellationToken);

        return value;
    }

    public Task InsertAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
    {
        return inner.InsertAsync(shortenedUrl, cancellationToken);
    }
}
