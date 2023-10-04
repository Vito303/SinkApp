using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sink.Client;
using Sink.Data;

namespace Sink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinkController : ControllerBase
    {
        private readonly mqtt_client _mqttClient;

        public SinkController(mqtt_client mqttClient)
        {
            _mqttClient = mqttClient;
        }

        [HttpGet]
        public async Task Get()
        {
            var response = HttpContext.Response;
            response.ContentType = "text/event-stream";

            await _mqttClient.Publish_Message("localhost", "sink/message", "dubledu");

            var request = await GetNextRequest();
            var data = JsonConvert.SerializeObject(request);
            await response.WriteAsync($"data: {data}\n\n");
        }

        private async Task<Request> GetNextRequest()
        {
            // Replace this with your logic to get the next request
            await Task.Delay(1000);
            return new Request { Method = "GET", Path = "/api/test", Time = DateTime.Now };
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Response responseData) 
        {
            var response = HttpContext.Response;
            response.ContentType = "text/event-stream";

            var json = JsonConvert.SerializeObject(responseData);
            
            await _mqttClient.Publish_Message("localhost", "sink/message", json);

            var request = await GetNextRequest();
            var data = JsonConvert.SerializeObject(request);
            await response.WriteAsync($"data: {data}\n\n");

            return Ok();
        }
    }
}