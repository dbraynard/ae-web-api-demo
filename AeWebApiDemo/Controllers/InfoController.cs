using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AeWebApiDemo.TypeFilters;

namespace AeWebApiDemo.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class InfoController : ControllerBase {
        private IWebHostEnvironment environment;

        public InfoController(IWebHostEnvironment _environment) {
            environment = _environment;
        }

        [HttpGet]
        [Produces("application/json")]
        [BasicAuth]
        public async Task<ActionResult<JsonDocument>> GetInfo() {
            var path = $"{environment.ContentRootPath}\\tableau-extension-info.json";
            var info = await System.IO.File.ReadAllTextAsync(path);
            var jsonDoc = JsonDocument.Parse(info);
            return Ok(jsonDoc);
        }
    }
}