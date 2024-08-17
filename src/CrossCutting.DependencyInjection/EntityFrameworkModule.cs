using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;

internal static class EntityFrameworkModule
{
    public static void AddEntityFrameworkModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");
        services.AddDbContext<DbContext, ApplicationDbContext>(builder => builder.UseSqlServer(connectionString));
    }
}
