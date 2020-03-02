using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TablesAPI;
using Xunit;

namespace ApiTests
{
    public class RowRequest
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
    
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            _factory = factory;
            factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile(configPath);
                });
            });
        }
        
        [Fact]
        public async Task PutThingInTable_AddsRowToTable()
        {            
            var client = _factory.CreateClient();

            var tableName = "basic_things";
            var thingName = "my thing";
            var thingAmount = 12;


            var requestJson = JsonConvert.SerializeObject(new RowRequest
            {
                Name = thingName,
                Amount = thingAmount
            });

            var response = await client.PostAsync(
                $"/api/tables/{tableName}/records", 
                new StringContent(requestJson, Encoding.UTF8, "application/json"));
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
        }
    }
}