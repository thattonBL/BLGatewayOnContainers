using MediatR;
using ABRSWebApi.Application.Behaviours;
using IntegrationEventLogEF;
using Microsoft.Extensions.Options;
using Message.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using ABRSWebApi.Application.IntegrationEvents;
using Services.Common;
using IntegrationEventLogEF.Services;
using System.Data.Common;
using Message.Infrastructure.Repositories;

namespace ABRSWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adds the Event Bus required for integration events
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

            builder.Services.AddDbContext<MessageContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(sp => (DbConnection c) => new IntegrationEventLogService(c));

            builder.Services.AddTransient<IMessageIntegrationEventService, MessageIntegrationEventService>();

            var services = builder.Services;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

                //cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                //cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });
            
            services.AddScoped<IMessageRepository, MessageRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MessageContext>();
                 //var env = app.Services.GetService<IWebHostEnvironment>();
                //var settings = app.Services.GetService<IOptions<OrderingSettings>>();
                //var logger = app.Services.GetService<ILogger<OrderingContextSeed>>();
                //await context.Database.MigrateAsync();

                //await new OrderingContextSeed().SeedAsync(context, env, settings, logger);
                //var integEventContext = scope.ServiceProvider.GetRequiredService<IntegrationEventLogContext>();
                //await integEventContext.Database.MigrateAsync();
            }

            app.MapControllers();
            app.Run();
        }
    }
}
