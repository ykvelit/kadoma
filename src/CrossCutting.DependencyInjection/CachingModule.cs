using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykvelit.Core.Caching;
using Ykvelit.Extensions.Caching.Distributed;

namespace CrossCutting.DependencyInjection;

internal static class CachingModule
{
    public static void AddCachingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICache, Cache>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "kadoma:";
        });
    }
}
