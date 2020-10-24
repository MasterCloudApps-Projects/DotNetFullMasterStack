using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.API.Infrastructure;
using Restaurant.API.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [Route("api/v1/restaurant")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> logger;
        private readonly RestaurantContext restaurantContext;

        public RestaurantController(RestaurantContext restaurantContext, ILogger<RestaurantController> logger)
        {
            this.restaurantContext = restaurantContext;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateRestaurantAsync([FromBody] Models.Restaurant restaurant)
        {
            restaurantContext.Restaurants.Add(restaurant);

            await restaurantContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurantAsync), new { id = restaurant.Id }, restaurant);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteRestaurantAsync(int id)
        {
            var Restaurant = restaurantContext.Restaurants.SingleOrDefault(x => x.Id == id);

            if (Restaurant == null)
            {
                return NotFound();
            }

            restaurantContext.Restaurants.Remove(Restaurant);

            await restaurantContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Models.Restaurant), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Models.Restaurant>> GetRestaurantAsync(int id)
        {
            var restaurant = await restaurantContext.Restaurants.SingleOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Models.Restaurant>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Models.Restaurant>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PaginatedItemsViewModel<Models.Restaurant>>> GetRestaurantsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await restaurantContext.Restaurants
                .LongCountAsync();

            var itemsOnPage = await restaurantContext.Restaurants
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<Models.Restaurant>(pageIndex, pageSize, totalItems, itemsOnPage);
        }
    }
}