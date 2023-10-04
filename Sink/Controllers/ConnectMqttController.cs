using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sink.Client;
using Sink.Data;

namespace Sink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectMqttController : ControllerBase
    {
        private readonly mqtt_client _mqttClient;

        public ConnectMqttController(mqtt_client mqttClient)
        {
            _mqttClient = mqttClient;
        }


        [HttpGet]
        public async Task Get()
        {
            var response = HttpContext.Response;
            response.ContentType = "text/event-stream";

            var request = await DoConnect(_mqttClient);
            var data = JsonConvert.SerializeObject(request);
            await response.WriteAsync($"data: {data}\n\n");
        }

        private static async Task<Request> DoConnect(mqtt_client mqttClient)
        {
            mqttClient.Connector();
            
            await Task.Delay(1000);
            return new Request { Method = "DoConnect", Path = "/api/ConnectMqtt", Message = "Done", Time = DateTime.Now };
        }
    }
}