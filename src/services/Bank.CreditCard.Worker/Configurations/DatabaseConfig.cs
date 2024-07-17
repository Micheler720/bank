using System.Diagnostics.CodeAnalysis;
using Bank.CreditCard.Worker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bank.CreditCard.Worker.Configurations; 

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