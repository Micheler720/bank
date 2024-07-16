using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace Bank.Proposals.Configurations;

[ExcludeFromCodeCoverage]
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