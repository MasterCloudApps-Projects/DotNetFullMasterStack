using Kitchen.API.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Management.Endpoint;

namespace Kitchen.API
{
    public class Program
    {
        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .AddAllActuators()
                .Build();

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);

            host.MigrateDbContext<KitchenContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<KitchenContextSeed>>();

                new KitchenContextSeed()
                    .SeedAsync(context, logger)
                    .Wait();
            });

            host.Run();
        }
    }
}