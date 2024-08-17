using Ykvelit.Extensions.MediatR.Behaviors;

namespace Application.Queries.GetShortenedUrl;

public class GetShortenedUrlQueryCachePolicy : IRequestCachePolicy<GetShortenedUrlQuery, string>
{
    public DateTime? AbsoluteExpiration => null;

    public TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(1);

    public TimeSpan? SlidingExpiration => TimeSpan.FromSeconds(30);
}
