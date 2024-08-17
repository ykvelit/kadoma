using Data.Specifications;
using Domain.Entities;
using Domain.Repositories;
using Ykvelit.Core.Data;

namespace Data.Repositories;
public class ShortenedUrlRepository(IRepository<ShortenedUrl> shortenedUrls) : IShortenedUrlRepository
{
    public async Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken)
    {
        var spec = new GetShortenedUrlByCode(code);
        var shortenedUrl = await shortenedUrls.FindAsync(spec, false, cancellationToken);
        return shortenedUrl is not null;
    }

    public async Task<ShortenedUrl> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var spec = new GetShortenedUrlByCode(code);
        var shortenedUrl = await shortenedUrls.FindAsync(spec, true, cancellationToken);
        return shortenedUrl!;
    }

    public Task InsertAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
    {
        return shortenedUrls.InsertAsync(shortenedUrl, cancellationToken);
    }
}
