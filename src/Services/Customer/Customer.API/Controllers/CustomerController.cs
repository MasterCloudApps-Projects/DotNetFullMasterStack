using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Customer.API.Infrastructure;
using Customer.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customer.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ILogger<CustomerController> logger;
        private readonly CustomerContext customerContext;

        public CustomerController(CustomerContext customerContext, ILogger<CustomerController> logger)
        {
            this.customerContext = customerContext;
            this.logger = logger;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateCustomerAsync([FromBody] Models.Customer customer)
        {
            customerContext.Customers.Add(customer);

            await customerContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomerAsync), new { id = customer.Id }, customer);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Models.Customer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Models.Customer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PaginatedItemsViewModel<Models.Customer>>> GetCustomersAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await customerContext.Customers
                .LongCountAsync();

            var itemsOnPage = await customerContext.Customers
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<Models.Customer>(pageIndex, pageSize, totalItems, itemsOnPage);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Models.Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ActionName("GetCustomerAsync")]
        public async Task<ActionResult<Models.Customer>> GetCustomerAsync(int id)
        {

            var customer = await customerContext.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteCustomerAsync(int id)
        {
            var customer = customerContext.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            customerContext.Customers.Remove(customer);

            await customerContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
