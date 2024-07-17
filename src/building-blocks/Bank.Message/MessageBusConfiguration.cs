using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Message;

[ExcludeFromCodeCoverage]
public static class MessageBusConfiguration
{
    public static IServiceCollection AddMessageBus(
        this IServiceCollection services,
        List<ProducerConfiguration>? producerConfigurations = null,
        List<ConsumerConfiguration>? consumerConfigurations = null)
    {
        var user = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "";
        var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "";
        var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "";

        if (string.IsNullOrEmpty(host)) throw new ArgumentNullException();
        

        services.AddMassTransit(x =>
        {
            consumerConfigurations?.ForEach(consumer =>
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

                producerConfigurations?.ForEach(producer =>
                {
                    ConfigureProducer(cfg, producer.QueueName, producer.ProducerType);
                });
                
                consumerConfigurations?.ForEach(consumer =>
                {
                    cfg.ReceiveEndpoint(consumer.QueueName, e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.AutoDelete = false;
                        e.Durable = true;
                        e.PrefetchCount = 10;
                        e.UseMessageRetry(r => { 
                            r.Interval(consumer.CountRetry, TimeSpan.FromSeconds(60));
                            r.Ignore<MessageConsumedException>();
                            r.Ignore<ArgumentNullException>();
                        });
                        

                        e.ConfigureConsumer(context, consumer.ConsumerType);
                    });
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IMessageBus, MessageBus>();

        return services;
    }

    private static void ConfigureProducer(
        IRabbitMqBusFactoryConfigurator cfg, 
        string queueName,
        Type messageType)
    {
        var method = typeof(MessageBusConfiguration).GetMethod(nameof(ProducerConfiguration), BindingFlags.Static | BindingFlags.NonPublic);
        var genericMethod = method.MakeGenericMethod(messageType);

        genericMethod.Invoke(null, [cfg, queueName]);
    }

    private static void ProducerConfiguration<T>(
        IRabbitMqBusFactoryConfigurator cfg, 
        string queueName) 
        where T : class
    {
        cfg.Publish<T>(p =>
        {
            var exchangeName = p.Exchange.ExchangeName;

            p.BindQueue(exchangeName, queueName,e =>
            {
                e.ExchangeType = "fanout";
                e.RoutingKey = nameof(T);
            });

            cfg.Publish<T>(c => c.ExchangeType = "fanout");

            cfg.MessageTopology.SetEntityNameFormatter(
                new PrefixEntityNameFormatter(cfg.MessageTopology.EntityNameFormatter, exchangeName));
            
            cfg.Send<T>(c => 
            {
                c.UseRoutingKeyFormatter(rk => rk.Message.GetType().Name);
            });

        });
    }
}