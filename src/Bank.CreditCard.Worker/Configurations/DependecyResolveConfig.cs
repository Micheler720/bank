using Bank.CreditCard.Worker.Domain;

namespace Bank.CreditCard.Worker.Configurations;

public static class DependecyResolveConfig
{
    public static void AddDependecyResolver(this IServiceCollection services)
    {
        services.ResolveServices();
    }

    private static void ResolveServices(this IServiceCollection services)
    {
        services.AddScoped<ICreditCardService, CreditCardService>();
    }


}