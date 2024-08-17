using Domain.Entities;
using Ykvelit.Core.Data;

namespace Data.Specifications;
internal class GetShortenedUrlByCode : Specification<ShortenedUrl>
{
    public GetShortenedUrlByCode(string code)
    {
        AddFilter(x => x.Code == code);
    }
}
