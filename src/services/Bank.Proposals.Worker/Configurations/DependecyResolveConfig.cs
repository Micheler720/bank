using System.Diagnostics.CodeAnalysis;
using Bank.Proposals.Worker.Domain.HttpService;
using Bank.Proposals.Worker.Domain.Services;
using Bank.Proposals.Worker.Domain.Services.Interface;
using Bank.Proposals.Worker.Infrastructure.HttpServices;

namespace Bank.Proposals.Configurations;

[ExcludeFromCodeCoverage]
public static class DependecyResolveConfig
{
    public static void AddDependecyResolver(this IServiceCollection services)
    {
        services.ResolveServices();
        services.ResolveHttpServices();
    }

    private static void ResolveServices(this IServiceCollection services)
    {
        services.AddScoped<IProposalService, ProposalService>();
    }

    private static void ResolveHttpServices(this IServiceCollection services)
    {
        services.AddScoped<IScoreHttpService, ScoreHttpService>();
    }

}