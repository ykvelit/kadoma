using Ykvelit.Core;
using Ykvelit.Core.Domain.Entities;

namespace Domain.Entities;
public class ShortenedUrl : Entity
{
    public const int CodeLength = 7;

    public ShortenedUrl(string longUrl, string code) : base()
    {
        Check.NotEmpty(longUrl, nameof(longUrl));
        Check.NotEmpty(code, nameof(code));

        LongUrl = longUrl;
        Code = code;
        CreatedAt = DateTime.Now;
    }

    public string LongUrl { get; set; } 
    public string Code { get; set; } 
    public DateTime CreatedAt { get; set; }
}
