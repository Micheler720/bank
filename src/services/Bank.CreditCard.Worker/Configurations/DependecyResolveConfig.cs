using System.Diagnostics.CodeAnalysis;
using Bank.CreditCard.Worker.Domain;
using Bank.CreditCard.Worker.Infrastructure.Data.Repositories;

namespace Bank.CreditCard.Worker.Configurations;

[ExcludeFromCodeCoverage]
public static class DependecyResolveConfig
{
    public static void AddDependecyResolver(this IServiceCollection services)
    {
        services.ResolveServices();
        services.ResolveRepositories();
    }

    private static void ResolveServices(this IServiceCollection services)
    {
        services.AddScoped<ICreditCardService, CreditCardService>();
    }

    private static void ResolveRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICreditCardRepository, CreditCardRepository>();
    }


}