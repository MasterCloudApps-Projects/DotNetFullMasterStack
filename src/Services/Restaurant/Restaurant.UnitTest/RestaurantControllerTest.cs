using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.API.Controllers;
using Restaurant.API.Infrastructure;
using Restaurant.API.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.UnitTest
{
    public class RestaurantControllerTest
    {
        private readonly DbContextOptions<RestaurantContext> dbContextOptions;

        private readonly ILogger<RestaurantController> logger;

        public RestaurantControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<RestaurantContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new RestaurantContext(dbContextOptions))
            {
                dbContext.AddRange(GetFakeData());
                dbContext.SaveChanges();
            }
            logger = new Mock<ILogger<RestaurantController>>().Object;
        }

        [Fact]
        public async Task Get_restaurants_success()
        {
            //Arrange
            var pageSize = 4;
            var pageIndex = 1;

            var expectedItemsInPage = 0;
            var expectedTotalItems = 1;

            var restaurantController = new RestaurantContext(dbContextOptions);

            //Act
            var orderingController = new RestaurantController(restaurantController, logger);
            var actionResult = await orderingController.GetRestaurantsAsync(pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedItemsViewModel<API.Models.Restaurant>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<API.Models.Restaurant>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        private List<API.Models.Restaurant> GetFakeData()
        {
            return new List<API.Models.Restaurant>()
            {
                new API.Models.Restaurant()
                {
                    Id = 1,
                    Name = "Menu 1",
                    Menus = new List<API.Models.Menu>()
                    {
                        new API.Models.Menu()
                        {
                            Id = 1,
                            Name = "Menu 1",
                            MenuItems = new List<API.Models.MenuItem>()
                            {
                                new API.Models.MenuItem()
                                {
                                    Id= 1,
                                    Name = "Plate 1",
                                    Price = 10
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}