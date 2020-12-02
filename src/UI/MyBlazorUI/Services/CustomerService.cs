using Microsoft.Extensions.Logging;
using MyBlazorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyBlazorUI.Services
{
    public class CustomerService : BaseDiscoveryService, ICustomerService
    {
        private readonly string GATEWAY_URL = Environment.GetEnvironmentVariable("GATEWAY_URL");
        private readonly string CUSTOMER_SERVICE_URL = Environment.GetEnvironmentVariable("CUSTOMER_SERVICE_URL");

        public CustomerService(HttpClient client) : base(client, null)
        {
        }

        public async Task<PaginatedItemsViewModel<Customer>> GetTicketsAsync()
        {
            var result = await Invoke<PaginatedItemsViewModel<Customer>>(new HttpRequestMessage(HttpMethod.Get, $"{GATEWAY_URL}{CUSTOMER_SERVICE_URL}"));
            return result; 
        }
    }
}
