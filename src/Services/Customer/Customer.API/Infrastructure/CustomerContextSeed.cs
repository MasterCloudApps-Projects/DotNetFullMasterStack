using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.API.Infrastructure
{
    public class CustomerContextSeed
    {
        public async Task SeedAsync(CustomerContext context, ILogger<CustomerContextSeed> logger, int retries = 3)
        {
            var policy = CreatePolicy(retries, logger, nameof(CustomerContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Customers.Any())
                {
                    await context.Customers.AddRangeAsync(
                        GetPreconfiguredCustomers());

                    await context.SaveChangesAsync();
                }
            });
        }

        private List<Models.Customer> GetPreconfiguredCustomers()
        {
            return new List<Models.Customer>
            {
                new Models.Customer
                {
                    Name = "Javier",
                    Surname = "Vela"
                },
                new Models.Customer
                {
                    Name = "Name1",
                    Surname = "Surname1"
                }
            };
        }

        private AsyncRetryPolicy CreatePolicy(int retries, ILogger<CustomerContextSeed> logger, string prefix)
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
