using Serilog;

namespace Bank.Proposals.Configurations;
public static class LogConfig
{
    public static IServiceCollection AddLogConfig(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        return services;
    }

}