using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;

internal static class DomainModule
{
    public static void AddDomainModule(this IServiceCollection services)
    {
        services.AddSingleton<IUniqueCodeGenerator, UniqueCodeGenerator>();
        services.AddScoped<IShortenedUrlUniqueCodeGenerator, ShortenedUrlUniqueCodeGenerator>();
    }
}
