using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.API;
using Ordering.API.Infrastructure;
using System.IO;
using System.Reflection;

namespace Ordering.FunctionalTests
{
    public abstract class OrderingScenarioBase
    {
        public static string OrderingsUrlBase => "api/v1/ordering";

        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(OrderingScenarioBase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                      .AddEnvironmentVariables();
                })
                .CaptureStartupErrors(true)
                .UseStartup<Startup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host
               .MigrateDbContext<OrderingContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<OrderingContextSeed>>();

                   new OrderingContextSeed()
                       .SeedAsync(context, logger)
                       .Wait();
               });

            return testServer;
        }

        public static class Delete
        {
            public static string DeleteOrdering(int id)
                => $"{OrderingsUrlBase}/{id}";
        }

        public static class Get
        {
            public static string Orderings = OrderingsUrlBase;

            public static string OrderingById(int id) => $"{OrderingsUrlBase}/{id}";
        }

        public static class Post
        {
            public static string AddNewOrdering = OrderingsUrlBase;
        }

        public static class Put
        {
            public static string UpdateOrdering(int id) => $"{OrderingsUrlBase}/{id}";
        }
    }
}