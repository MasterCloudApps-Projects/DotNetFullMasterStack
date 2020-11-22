
using Customer.API.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Management.Endpoint;

namespace Customer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);

            host.MigrateDbContext<CustomerContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<CustomerContextSeed>>();

                new CustomerContextSeed()
                    .SeedAsync(context,  logger)
                    .Wait();
            });

            host.Run();
        }

        public static IWebHost CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .AddAllActuators()
                .Build();
    }
}
