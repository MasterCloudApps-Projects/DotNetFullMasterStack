using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.API.Infrastructure;

namespace Ordering.API
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

            host.MigrateDbContext<OrderingContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<OrderingContextSeed>>();

                new OrderingContextSeed()
                    .SeedAsync(context, logger)
                    .Wait();
            });

            host.Run();
        }
    }
}