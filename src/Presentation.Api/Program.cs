using CrossCutting.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Ykvelit.Extensions.AspNetCore.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        ;
});

builder.Services.AddResponseCompression();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("SqlServer")!,
        name: "sql-server",
        failureStatus: HealthStatus.Degraded,
        tags: ["db", "data", "sql", "sql-server"]
    )
    .AddRedis(
        redisConnectionString: builder.Configuration.GetConnectionString("Redis")!,
        name: "redis",
        failureStatus: HealthStatus.Degraded,
        tags: ["cache", "redis"]
    )
    ;

builder.Services.AddRootModule(builder.Configuration);

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<UnauthorizedExceptionHandler>();
builder.Services.AddExceptionHandler<UserFriendlyExceptionHandler>();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<DbContext>();
    context.Database.Migrate();
}

app.UseSerilogRequestLogging();

app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.MapControllers();

app.MapHealthChecks("/_/health");

app.Run();
