using Kitchen.API.Controllers;
using Kitchen.API.Infrastructure;
using Kitchen.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kitchen.UnitTest
{
    public class KitchenControllerTest
    {
        private readonly DbContextOptions<KitchenContext> dbContextOptions;

        private readonly ILogger<KitchenController> logger;

        public KitchenControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<KitchenContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new KitchenContext(dbContextOptions))
            {
                dbContext.AddRange(GetFakeData());
                dbContext.SaveChanges();
            }
            logger = new Mock<ILogger<KitchenController>>().Object;
        }

        [Fact]
        public async Task Get_tickets_success()
        {
            //Arrange
            var pageSize = 4;
            var pageIndex = 1;

            var expectedItemsInPage = 0;
            var expectedTotalItems = 1;

            var kitchenContext = new KitchenContext(dbContextOptions);

            //Act
            var kitchenController = new KitchenController(kitchenContext, logger);
            var actionResult = await kitchenController.GetTicketsAsync(pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedItemsViewModel<API.Models.Ticket>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<API.Models.Ticket>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        private List<API.Models.Ticket> GetFakeData()
        {
            return new List<API.Models.Ticket>()
            {
                new API.Models.Ticket()
                {
                    RestaurantId = 1,
                    TicketLines=new List<API.Models.TicketLine>()
                    {
                        new API.Models.TicketLine()
                        {
                            Id = 1,
                            Name ="Tasty",
                            Quantity = 2
                        }
                    }
                }
              
            };
        }
    }
}