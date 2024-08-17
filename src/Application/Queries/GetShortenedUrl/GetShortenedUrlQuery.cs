using Ykvelit.Core.Application.Queries;

namespace Application.Queries.GetShortenedUrl;
public record GetShortenedUrlQuery(string Code) : IQuery<string>;
