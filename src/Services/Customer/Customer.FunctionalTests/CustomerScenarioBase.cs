using Customer.API;
using Customer.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace Customer.FunctionalTests
{
    public abstract class CustomerScenarioBase
    {
        public static string UrlBase => "api/v1/customer";

        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CustomerScenarioBase))
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
               .MigrateDbContext<CustomerContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<CustomerContextSeed>>();

                   new CustomerContextSeed()
                       .SeedAsync(context, logger)
                       .Wait();
               });

            return testServer;
        }


        public static class Get
        {
            public static string Customers = UrlBase;

            public static string CustomerById(int id) => $"{UrlBase}/{id}";

        }

        public static class Post
        {
            public static string AddNewCustomer = UrlBase;
        }

        public static class Put
        {
            public static string UpdateCustomer(int id) => $"{UrlBase}/{id}";
        }

        public static class Delete
        {
            public static string DeleteCustomer(int id)
                => $"{UrlBase}/{id}";
        }
    }
}