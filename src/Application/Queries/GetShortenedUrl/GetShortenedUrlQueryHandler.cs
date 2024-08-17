using Domain.Repositories;
using Ykvelit.Core.Application.Queries;

namespace Application.Queries.GetShortenedUrl;

public class GetShortenedUrlQueryHandler(IShortenedUrlRepository shortenedUrls) : IQueryHandler<GetShortenedUrlQuery, string>
{
    public async Task<string> Handle(GetShortenedUrlQuery request, CancellationToken cancellationToken)
    {
        var result = await shortenedUrls.GetByCodeAsync(request.Code, cancellationToken);
        return result.LongUrl;
    }
}
