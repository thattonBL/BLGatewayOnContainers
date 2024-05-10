using EventBus.Abstractions;
using GatewayGrpcService.IntegrationEvents.EventHandling;
using GatewayGrpcService.IntegrationEvents.Events;
using GatewayGrpcService.Queries;
using GatewayGrpcService.Services;
using Services.Common;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GatewayGrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Adds the event Bus / RabbitMQ
            builder.AddServiceDefaults();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

            if (connectionString != null)
            {
                connectionString = connectionString.Replace("{#host}", dbHost).Replace("{#dbName}", dbName).Replace("{#dbPassword}", dbPassword);
            }

            builder.Services.AddScoped<IGatewayRequestQueries>(sp => new GatewayRequestQueries(connectionString));
            builder.Services.AddGrpc().AddJsonTranscoding();
            builder.Services.AddGrpcReflection();
            builder.Services.AddGrpcSwagger();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Gateway Grpc Service", Version = "v1" });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddTransient<NewRsiMessageSubmittedIntegrationEventHandler>();
            builder.Services.AddTransient<StopConsumerRequestIntegrationEventHandler>();

            var app = builder.Build();

            app.Use((context, next) =>
            {
                context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                return next.Invoke();
            });
            app.UseSwagger();

            var contentRoot = builder.Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "gRPC using .NET7 Demo");
                //c.ConfigObject.Urls = new[] { new UrlDescriptor { Name = "gRPC", Url = Path.Combine(contentRoot, "swagger/custom-swagger-config.json") }};
            });

            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.
            app.MapGrpcService<RsiMessageService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            app.MapSwagger();
            app.MapGrpcReflectionService();

            var eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<NewRsiMessageSubmittedIntegrationEvent, NewRsiMessageSubmittedIntegrationEventHandler>(NewRsiMessageSubmittedIntegrationEvent.EVENT_NAME);
            eventBus.Subscribe<StopConsumerRequestIntegrationEvent, StopConsumerRequestIntegrationEventHandler>(StopConsumerRequestIntegrationEvent.EVENT_NAME);

            app.Run();
        }
    }
}