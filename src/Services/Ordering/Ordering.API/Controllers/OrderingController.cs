using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.API.Infrastructure;
using Ordering.API.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderingController : Controller
    {
        private readonly ILogger<OrderingController> logger;
        private readonly OrderingContext orderingContext;

        public OrderingController(OrderingContext orderingContext, ILogger<OrderingController> logger)
        {
            this.orderingContext = orderingContext;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateRestaurantAsync([FromBody] Models.Order order)
        {
            orderingContext.Orders.Add(order);

            await orderingContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderAsync), new { id = order.Id }, order);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteRestaurantAsync(int id)
        {
            var Restaurant = orderingContext.Orders.SingleOrDefault(x => x.Id == id);

            if (Restaurant == null)
            {
                return NotFound();
            }

            orderingContext.Orders.Remove(Restaurant);

            await orderingContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Models.Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Models.Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PaginatedItemsViewModel<Models.Order>>> GetOrdersAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await orderingContext.Orders
                .LongCountAsync();

            var itemsOnPage = await orderingContext.Orders
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<Models.Order>(pageIndex, pageSize, totalItems, itemsOnPage);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Models.Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Models.Order>> GetOrderAsync(int id)
        {
            var order = await orderingContext.Orders.SingleOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}