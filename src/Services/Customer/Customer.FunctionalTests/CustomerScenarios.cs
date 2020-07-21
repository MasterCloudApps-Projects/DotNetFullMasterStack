using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customer.FunctionalTests
{
    public class CustomerScenarios : CustomerScenarioBase
    {
        [Fact]
        public async Task Delete_delete_customer_and_response_not_content_status_code()
        {
            using (var server = CreateServer())
            {
                var customer = GetFakeCustomer();
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                var responseAddCustomer = await server.CreateClient()
                    .PostAsync(Post.AddNewCustomer, content);

                if (int.TryParse(responseAddCustomer.Headers.Location.Segments[3], out int id))
                {
                    var response = await server.CreateClient().DeleteAsync(Delete.DeleteCustomer(id));

                    Assert.True(response.StatusCode == HttpStatusCode.NoContent);
                }

                responseAddCustomer.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Get_get_all_customers_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.Customers);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Get_get_customer_by_id_and_response_not_found_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.CustomerById(int.MaxValue));

                Assert.True(response.StatusCode == HttpStatusCode.NotFound);
            }
        }

        [Fact]
        public async Task Get_get_customer_by_id_and_response_ok_status_code()
        {
            var customerId = 1;

            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.CustomerById(customerId));

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Post_add_new_customer_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var customer = GetFakeCustomer();
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = await server.CreateClient()
                    .PostAsync(Post.AddNewCustomer, content);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Put_update_customer_and_response_not_content_status_code()
        {
            using (var server = CreateServer())
            {
                var customer = GetFakeCustomer();
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                //add customer
                var customerResponse = await server.CreateClient()
                    .PostAsync(Post.AddNewCustomer, content);

                if (int.TryParse(customerResponse.Headers.Location.Segments[3], out int id))
                {
                    customer.Surname = "Giamelli";
                    content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                    var response = await server.CreateClient()
                    .PutAsync(Put.UpdateCustomer(id), content);

                    Assert.True(response.StatusCode == HttpStatusCode.Created);
                }

                customerResponse.EnsureSuccessStatusCode();
            }
        }

        private static API.Models.Customer GetFakeCustomer()
        {
            return new Customer.API.Models.Customer()
            {
                Name = "Allianora",
                Surname = "Eyeington"
            };
        }
    }
}