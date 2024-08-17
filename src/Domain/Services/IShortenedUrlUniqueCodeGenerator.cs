
namespace Domain.Services;

public interface IShortenedUrlUniqueCodeGenerator
{
    Task<string> GenerateAsync(CancellationToken cancellationToken);
}