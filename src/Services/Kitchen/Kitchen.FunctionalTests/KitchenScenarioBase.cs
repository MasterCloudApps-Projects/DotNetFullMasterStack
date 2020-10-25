using Kitchen.API;
using Kitchen.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace Kitchen.FunctionalTests
{
    public abstract class KitchenScenarioBase
    {
        public static string CustomersUrlBase => "api/v1/kitchen";

        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(KitchenScenarioBase))
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
               .MigrateDbContext<KitchenContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<KitchenContextSeed>>();

                   new KitchenContextSeed()
                       .SeedAsync(context, logger)
                       .Wait();
               });

            return testServer;
        }

        public static class Delete
        {
            public static string DeleteCustomer(int id)
                => $"{CustomersUrlBase}/{id}";
        }

        public static class Get
        {
            public static string Customers = CustomersUrlBase;

            public static string CustomerById(int id) => $"{CustomersUrlBase}/{id}";
        }

        public static class Post
        {
            public static string AddNewCustomer = CustomersUrlBase;
        }

        public static class Put
        {
            public static string UpdateCustomer(int id) => $"{CustomersUrlBase}/{id}";
        }
    }
}