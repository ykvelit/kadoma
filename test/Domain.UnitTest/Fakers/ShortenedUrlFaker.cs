using Bogus;
using Domain.Entities;

namespace Domain.UnitTest.Fakers;
public class ShortenedUrlFaker : Faker<ShortenedUrl>
{
    public ShortenedUrlFaker()
    {
        CustomInstantiator(faker =>
        {
            var longUrl = faker.Internet.Url();
            var code = faker.Random.String2(10);

            return new ShortenedUrl(longUrl, code);
        });
    }
}
