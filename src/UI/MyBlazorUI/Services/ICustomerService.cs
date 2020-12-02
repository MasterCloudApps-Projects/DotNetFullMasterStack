using MyBlazorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlazorUI.Services
{
   public interface ICustomerService
    {
        public Task<PaginatedItemsViewModel<Customer>> GetTicketsAsync();
    }
}
