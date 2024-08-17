using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services;

public class ShortenedUrlUniqueCodeGenerator(IShortenedUrlRepository shortenedUrls, IUniqueCodeGenerator uniqueCode) : IShortenedUrlUniqueCodeGenerator
{
    public async Task<string> GenerateAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            var code = uniqueCode.Generate(ShortenedUrl.CodeLength);

            var isUnique = await IsUniqueAsync(code, cancellationToken);
            if (isUnique)
            {
                return code;
            }
        }
    }

    private async Task<bool> IsUniqueAsync(string code, CancellationToken cancellationToken)
    {
        return !await shortenedUrls.CodeExistsAsync(code, cancellationToken);
    }
}