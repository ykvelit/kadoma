using Domain.Entities;

namespace Domain.Repositories;
public interface IShortenedUrlRepository
{
    Task InsertAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken);
    Task<ShortenedUrl> GetByCodeAsync(string code, CancellationToken cancellationToken);
    Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken);
}
