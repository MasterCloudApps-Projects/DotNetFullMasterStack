using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.API.Infrastructure
{

    public class KitchenContextSeed
    {
        public async Task SeedAsync(KitchenContext context, ILogger<KitchenContextSeed> logger, int retries = 3)
        {
            var policy = CreatePolicy(retries, logger, nameof(KitchenContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Tickets.Any())
                {
                    await context.Tickets.AddRangeAsync(
                        GetPreconfiguredTickets());

                    await context.SaveChangesAsync();
                }
            });
        }

        private List<Models.Ticket> GetPreconfiguredTickets()
        {
            return new List<Models.Ticket>
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

        private AsyncRetryPolicy CreatePolicy(int retries, ILogger<KitchenContextSeed> logger, string prefix)
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
