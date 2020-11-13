using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant.API.Infrastructure;

namespace Restaurant.API
{
    public class Program
    {
        public static IWebHost CreateHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .UseStartup<Startup>()
           .Build();

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);

            host.MigrateDbContext<RestaurantContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<RestaurantContextSeed>>();

                new RestaurantContextSeed()
                    .SeedAsync(context, logger)
                    .Wait();
            });

            host.Run();
        }
    }
}