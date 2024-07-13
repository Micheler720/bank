using System.Diagnostics.CodeAnalysis;
using Bank.Core.Messages.Integration;
using MassTransit;

namespace Bank.Clients.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class MessageConfiguration
{
    public static void AddMessageConfig(this IServiceCollection services)
    {
        var user = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "";
        var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "";
        var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "";

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(password);
                    h.PublisherConfirmation = true;
                });

                cfg.ConfigureEndpoints(context);
            });
        });

    }
}