using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Message.Infrastructure;

namespace ABRSWebApi.Infrastructure.Factories;

public class MessageDbContextFactory : IDesignTimeDbContextFactory<MessageContext>
{
    public MessageContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
           .AddJsonFile("appsettings.json")
           .AddEnvironmentVariables()
           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MessageContext>();

        optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Ordering.API"));

        return new MessageContext(optionsBuilder.Options);
    }
}
