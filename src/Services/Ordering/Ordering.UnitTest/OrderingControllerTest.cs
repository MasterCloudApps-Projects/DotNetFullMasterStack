using Esquio.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Ordering.API.Controllers;
using Ordering.API.Infrastructure;
using Ordering.API.ViewModel;
using Swashbuckle.SwaggerUi;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ordering.UnitTest
{
    public class OrderingControllerTest
    {
        private readonly DbContextOptions<OrderingContext> dbContextOptions;

        private readonly ILogger<OrderingController> logger;

        //private readonly IFeatureService featureService;

        public OrderingControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<OrderingContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new OrderingContext(dbContextOptions))
            {
                dbContext.AddRange(GetFakeData());
                dbContext.SaveChanges();
            }
            logger = new Mock<ILogger<OrderingController>>().Object;
            //
        }

        [Fact]
        public async Task Get_orders_success()
        {
            //Arrange
            var pageSize = 4;
            var pageIndex = 1;

            var expectedItemsInPage = 0;
            var expectedTotalItems = 1;

            var orderingContext = new OrderingContext(dbContextOptions);

            //Act
            var orderingController = new OrderingController(orderingContext, logger, null);
            var actionResult = await orderingController.GetOrdersAsync(pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedItemsViewModel<API.Models.Order>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<API.Models.Order>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        [Fact] 
        public void Get_Promo_Winter()
        {
            var expectedPromoWinter = "WINTER-PROMO";
            Mock<IFeatureService> featureService = new Mock<IFeatureService>();
            
            featureService.Setup(x => x.IsEnabledAsync("IsEnabledAsync", It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var orderingContext = new OrderingContext(dbContextOptions);

            //Act
            var orderingController = new OrderingController(orderingContext, logger, featureService.Object);
            var actionResult = orderingController.GetPromoWinter();
            Assert.Equal(expectedPromoWinter, actionResult);       

        }

        private List<API.Models.Order> GetFakeData()
        {
            return new List<API.Models.Order>()
            {
                new API.Models.Order()
                {
                    RestaurantId = 1,
                    CustomerId = 1,
                    DeliveryAddress= new API.Models.Address()
                    {
                        City = "Zgz",
                        Id = 1,
                        PostalCode = "50000",
                        StreetName = "Street 1"
                    },
                    OrderDetails = new List<API.Models.OrderLine>()
                    {
                        new API.Models.OrderLine()
                        {
                            Id = 1,
                            Quantity = 2,
                            Name = "Plate 1"
                        }
                    }
                }
            };
        }
    }
}