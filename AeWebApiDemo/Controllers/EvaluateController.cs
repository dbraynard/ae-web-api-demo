using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AeWebApiDemo.TypeFilters;
using AeWebApiDemo.Models;

namespace AeWebApiDemo.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class EvaluateController : ControllerBase {
        IHttpClientFactory httpClientFactory;

        public EvaluateController(IHttpClientFactory httpClientFactory) {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Accepts an analytics extension command from Tableau.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /Evaluate (Example 1):
        ///     {
        ///         "script":"Currency:EUR", 
        ///         "data":{}
        ///     }
        /// 
        ///     POST /Evaluate (Example 2):
        ///     {
        ///         "script": "Trending",
        ///          "data": {
        ///             "_arg1": ["Bike Racks", "Gloves", "Handlebars"]
        ///          }
        ///     }
        /// 
        ///     POST /Evaluate (Example 3):
        ///     {
        ///         "script": "Shipping",
        ///          "data": {
        ///             "_arg1": ["USA", "AU", "CA"],
        ///             "_arg2": ["22182", "5020", "G1R"]
        ///          }
        ///     }
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Result array of the command query</returns>
        [HttpPost]
        [BasicAuth]
        public async Task<ActionResult<object[]>> Post([FromBody] JsonElement request) {
            //Tableau Authentication/Settings requests this evaluation as a "ping" test.
            if (request.GetProperty(TableauKeywords.Script).GetString() == "return int(1)") {
                return Ok(new object[1] { 1 });
            }

            var command = TableauCommand.GetFromJsonElement(request);
            command.Query.HttpClientFactory = httpClientFactory;
            var results = await command.ExecuteAsync();
            return Ok(results);
        }
    }
}