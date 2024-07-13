using System.Diagnostics.CodeAnalysis;
using Bank.Proposals.Worker.Services;
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
            x.AddConsumer<ClientRegistredConsumer>(); 

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(password);
                });

                cfg.ReceiveEndpoint("client-registred-queue", e =>
                {
                    e.ConfigureConsumeTopology = false;
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.PrefetchCount = 10;
                    e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(60)));
                    
                    e.ConfigureConsumer<ClientRegistredConsumer>(context);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}