using Customer.API.Controllers;
using Customer.API.Infrastructure;
using Customer.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Customer.UnitTest
{
    public class CustomerControllerTest
    {
        private readonly DbContextOptions<CustomerContext> dbContextOptions;

        private readonly ILogger<CustomerController> logger;

        public CustomerControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new CustomerContext(dbContextOptions))
            {
                dbContext.AddRange(GetFakeData());
                dbContext.SaveChanges();
            }
            logger = new Mock<ILogger<CustomerController>>().Object;
        }

        [Fact]
        public async Task Get_customers_success()
        {
            //Arrange
            var pageSize = 4;
            var pageIndex = 1;

            var expectedItemsInPage = 2;
            var expectedTotalItems = 6;

            var customerContext = new CustomerContext(dbContextOptions);

            //Act
            var customerController = new CustomerController(customerContext, logger);
            var actionResult = await customerController.GetCustomersAsync(pageSize, pageIndex);

            //Assert
            Assert.IsType<ActionResult<PaginatedItemsViewModel<API.Models.Customer>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<API.Models.Customer>>(actionResult.Value);
            Assert.Equal(expectedTotalItems, page.Count);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
            Assert.Equal(expectedItemsInPage, page.Data.Count());
        }

        private List<API.Models.Customer> GetFakeData()
        {
            return new List<API.Models.Customer>()
            {
                new API.Models.Customer
                {
                    Name = "Javier",
                    Surname = "Vela"
                },
                new API.Models.Customer
                {
                    Name = "Name1",
                    Surname = "Surname1"
                },
               new API.Models.Customer
                {
                    Name = "Javier",
                    Surname = "Vela"
                },
                new API.Models.Customer
                {
                    Name = "Name1",
                    Surname = "Surname1"
                },
                  new API.Models.Customer
                {
                    Name = "Javier",
                    Surname = "Vela"
                },
                new API.Models.Customer
                {
                    Name = "Name1",
                    Surname = "Surname1"
                }
            };
        }
    }
}