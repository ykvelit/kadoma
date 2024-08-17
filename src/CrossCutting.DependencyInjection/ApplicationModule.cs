using Application.Commands.ShortenUrl;
using Application.Queries.GetShortenedUrl;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ykvelit.Extensions.MediatR.Behaviors;

namespace CrossCutting.DependencyInjection;

internal static class ApplicationModule
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddSingleton<IRequestCachePolicy<GetShortenedUrlQuery, string>, GetShortenedUrlQueryCachePolicy>();

        services.AddValidatorsFromAssembly(typeof(ShortenUrlCommand).Assembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ShortenUrlCommand).Assembly);

            config
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>))
                .AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                ;
        });
    }
}