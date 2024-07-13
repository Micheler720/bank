namespace Bank.Proposals.Configurations;

public static class DependecyResolveConfig
{
    public static void AddDependecyResolver(this IServiceCollection services)
    {
        services.ResolveRepositories();
        services.ResolveHttpServices();
    }

    private static void ResolveRepositories(this IServiceCollection services)
    {
    }

    private static void ResolveHttpServices(this IServiceCollection services)
    {
    }

}