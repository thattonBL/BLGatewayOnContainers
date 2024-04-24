using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
namespace Services.Common;

public static class CommonExtensions
{
    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    {
        // Shared configuration via key vault
        //builder.Configuration.AddKeyVault();

        // Shared app insights configuration
        //builder.Services.AddApplicationInsights(builder.Configuration);

        // Default health checks assume the event bus and self health checks
        //builder.Services.AddDefaultHealthChecks(builder.Configuration);

        // Add the event bus
        builder.Services.AddEventBus(builder.Configuration);

        //builder.Services.AddDefaultAuthentication(builder.Configuration);

        //builder.Services.AddDefaultOpenApi(builder.Configuration);

        // Add the accessor
        builder.Services.AddHttpContextAccessor();

        return builder;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        //  {
        //    "ConnectionStrings": {
        //      "EventBus": "..."
        //    },

        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus | RabbitMQ",
        //     ...
        //   }
        // }

        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus",
        //     "SubscriptionClientName": "eshop_event_bus"
        //   }
        // }

        // {
        //   "EventBus": {
        //     "ProviderName": "RabbitMQ",
        //     "SubscriptionClientName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1
        //   }
        // }

        var eventBusSection = configuration.GetSection("EventBus");

        if (!eventBusSection.Exists())
        {
            return services;
        }

        if (string.Equals(eventBusSection["ProviderName"], "ServiceBus", StringComparison.OrdinalIgnoreCase))
        {
            //services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            //{
            //    var serviceBusConnectionString = configuration.GetRequiredConnectionString("EventBus");

            //    return new DefaultServiceBusPersisterConnection(serviceBusConnectionString);
            //});

            //services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            //{
            //    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
            //    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
            //    var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            //    string subscriptionName = eventBusSection.GetRequiredValue("SubscriptionClientName");

            //    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
            //        eventBusSubscriptionsManager, sp, subscriptionName);
            //});
        }
        else
        {
            services.AddSingleton<IDefaultRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBus:HostName"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
                {
                    factory.UserName = eventBusSection["UserName"];
                }

                if (!string.IsNullOrEmpty(eventBusSection["Password"]))
                {
                    factory.Password = eventBusSection["Password"];
                }

                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = eventBusSection["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IDefaultRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
            });
        }

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        return services;
    }
}
