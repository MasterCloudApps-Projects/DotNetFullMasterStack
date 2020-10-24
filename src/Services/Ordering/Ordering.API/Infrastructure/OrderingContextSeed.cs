using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Ordering.API.Models;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ordering.API.Infrastructure
{
    public class OrderingContextSeed
    {
        public async Task SeedAsync(OrderingContext context, ILogger<OrderingContextSeed> logger, int retries = 3)
        {
            var policy = CreatePolicy(retries, logger, nameof(OrderingContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Orders.Any())
                {
                    await context.Orders.AddRangeAsync(
                        GetPreconfiguredOrders());

                    await context.SaveChangesAsync();
                }
            });
        }

        private List<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
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

        private AsyncRetryPolicy CreatePolicy(int retries, ILogger<OrderingContextSeed> logger, string prefix)
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
