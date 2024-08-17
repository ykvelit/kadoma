using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Ykvelit.Core.Application.Commands;

namespace Application.Commands.ShortenUrl;

public class ShortenUrlCommandHandler(IShortenedUrlUniqueCodeGenerator uniqueCode, IShortenedUrlRepository shortenedUrls) : ICommandHandler<ShortenUrlCommand, string>
{
    public async Task<string> Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
    {
        var code = await uniqueCode.GenerateAsync(cancellationToken);
        var shortenedUrl = new ShortenedUrl(request.Url, code);

        await shortenedUrls.InsertAsync(shortenedUrl, cancellationToken);

        return code;
    }
}
