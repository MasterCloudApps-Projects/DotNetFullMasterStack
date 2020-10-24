using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Restaurant.API;
using Restaurant.API.Infrastructure;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.FunctionalTests
{

    public abstract class RestaurantScenarioBase
    {
        public static string UrlBase => "api/v1/restaurant";

        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(RestaurantScenarioBase))
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
               .MigrateDbContext<RestaurantContext>((context, services) =>
               {
                   var logger = services.GetService<ILogger<RestaurantContextSeed>>();

                   new RestaurantContextSeed()
                       .SeedAsync(context, logger)
                       .Wait();
               });

            return testServer;
        }


        public static class Get
        {
            public static string Restaurants = UrlBase;

            public static string RestaurantById(int id) => $"{UrlBase}/{id}";

        }

        public static class Post
        {
            public static string AddNewRestaurant = UrlBase;
        }

        public static class Put
        {
            public static string UpdateRestaurant(int id) => $"{UrlBase}/{id}";
        }

        public static class Delete
        {
            public static string DeleteRestaurant(int id)
                => $"{UrlBase}/{id}";
        }
    }
}
