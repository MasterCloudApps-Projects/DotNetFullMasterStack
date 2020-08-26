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
        public async Task<ActionResult> CreateCustoemrAsync([FromBody] Models.Customer customer)
        {
        

            customerContext.Customers.Add(customer);

            await customerContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = customer.Id }, null);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Models.Customer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Models.Customer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PaginatedItemsViewModel<Models.Customer>>> GetProductsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
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
        public async Task<ActionResult<Models.Customer>> GetProduct(int id)
        {

            var product = await customerContext.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            var product = customerContext.Customers.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            customerContext.Customers.Remove(product);

            await customerContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
