using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TablesAPI.Controllers
{
    public class RowRequest
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }

    public class TablesSettings
    {
        public string ConnectionString { get; set; }
    }
    
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {

        private TablesSettings _settings;
        
        public TablesController(IOptions<TablesSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        
        [HttpPost("{tableName}/records")]
        public IActionResult CreateRow(string tableName, [FromBody] RowRequest request)
        {
            var connectionString = _settings.ConnectionString;
            return Ok();
        }
    }
}