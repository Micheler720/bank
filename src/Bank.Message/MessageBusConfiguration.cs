using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Message;
public static class MessageBusConfiguration
{
    public static IServiceCollection AddMessageBus(
        this IServiceCollection services,
        List<ConsumerConfiguration>? consumerConfigs = null)
    {
        var user = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "";
        var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "";
        var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "";

        if (string.IsNullOrEmpty(host)) throw new ArgumentNullException();

        services.AddMassTransit(x =>
        {
            consumerConfigs?.ForEach(consumer =>
            {
                x.AddConsumer(consumer.ConsumerType);
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(password);
                    h.PublisherConfirmation = true;
                });

                consumerConfigs?.ForEach(consumer =>
                {
                    cfg.ReceiveEndpoint(consumer.QueueName, e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.AutoDelete = false;
                        e.Durable = true;
                        e.PrefetchCount = 10;
                        e.UseMessageRetry(r => r.Interval(consumer.CountRetry, TimeSpan.FromSeconds(60)));

                        e.ConfigureConsumer(context, consumer.ConsumerType);
                    });
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IMessageBus, MessageBus>();

        return services;
    }
}