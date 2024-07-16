using System.Diagnostics.CodeAnalysis;
using Bank.Clients.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.Clients.Api.Configurations; 

[ExcludeFromCodeCoverage]
public static class DatabaseConfig
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DATABASE");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}