using System.Diagnostics.CodeAnalysis;
using Bank.Clients.Api.Application.Queries;
using Bank.Clients.Api.Domain;
using Bank.Clients.Api.Infrastructure.Data.Repositories;
using Bank.Core.Mediator;

namespace Bank.Clients.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class DependecyResolveConfig
{
    public static void AddDependecyResolver(this IServiceCollection services)
    {
        services.ResolveRepositories();
        services.ResolveQueries();

        services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static void ResolveRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
    }

    private static void ResolveQueries(this IServiceCollection services)
    {
        services.AddScoped<IClientQuery, ClientQuery>();
    }

}