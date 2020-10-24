using Kitchen.API.Infrastructure;
using Kitchen.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Kitchen.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class KitchenController : Controller
    {
        private readonly KitchenContext kitchenContext;
        private readonly ILogger<KitchenController> logger;

        public KitchenController(KitchenContext kitchenContext, ILogger<KitchenController> logger)
        {
            this.kitchenContext = kitchenContext;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateTicketAsync([FromBody] Models.Ticket ticket)
        {
            kitchenContext.Tickets.Add(ticket);

            await kitchenContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketsAsync), new { id = ticket.Id }, null);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteTicketAsync(int id)
        {
            var product = kitchenContext.Tickets.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            kitchenContext.Tickets.Remove(product);

            await kitchenContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Models.Ticket>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Models.Ticket>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PaginatedItemsViewModel<Models.Ticket>>> GetTicketsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await kitchenContext.Tickets
                .LongCountAsync();

            var itemsOnPage = await kitchenContext.Tickets
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<Models.Ticket>(pageIndex, pageSize, totalItems, itemsOnPage);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Models.Ticket), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Models.Ticket>> GetTicketsAsync(int id)
        {
            var product = await kitchenContext.Tickets.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}