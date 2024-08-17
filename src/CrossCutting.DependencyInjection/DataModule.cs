using Data.Repositories;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Ykvelit.Core.Caching;
using Ykvelit.Core.Data;
using Ykvelit.Extensions.Data.EntityFrameworkCore;

namespace CrossCutting.DependencyInjection;

internal static class DataModule
{
    public static void AddDataModule(this IServiceCollection services)
    {
        UnitOfWorkGlobalConfiguration.ShouldStoreEvents = false;
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services
            .AddScoped<ShortenedUrlRepository>()
            .AddScoped<IShortenedUrlRepository, CachedShortenedUrlRepository>(sp =>
            {
                var inner = sp.GetRequiredService<ShortenedUrlRepository>();
                var cache = sp.GetRequiredService<ICache>();

                return new CachedShortenedUrlRepository(inner, cache);
            })
            ;
    }
}