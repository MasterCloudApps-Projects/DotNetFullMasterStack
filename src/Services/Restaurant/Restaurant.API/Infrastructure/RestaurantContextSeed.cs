using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Restaurant.API.Models;


namespace Restaurant.API.Infrastructure
{
    public class RestaurantContextSeed
    {
        public async Task SeedAsync(RestaurantContext context, ILogger<RestaurantContextSeed> logger, int retries = 3)
        {
            var policy = CreatePolicy(retries, logger, nameof(RestaurantContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Restaurants.Any())
                {
                    await context.Restaurants.AddRangeAsync(
                        GetPreconfiguredRestaurants());

                    await context.SaveChangesAsync();
                }
            });
        }

        private List<Models.Restaurant> GetPreconfiguredRestaurants()
        {
            return new List<Models.Restaurant>
            {
                //new Models.Ticket
                //{

                //},
                //new Models.Ticket
                //{
                //    Name = "Name1",
                //    Surname = "Surname1"
                //}
            };
        }

        private AsyncRetryPolicy CreatePolicy(int retries, ILogger<RestaurantContextSeed> logger, string prefix)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
