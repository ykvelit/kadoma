﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;
public static class RootModule
{
    public static void AddRootModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomainModule();
        services.AddApplicationModule();
        services.AddDataModule();

        services.AddCachingModule(configuration);
        services.AddEntityFrameworkModule(configuration);
    }
}
