using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;

namespace Sink.Client
{
    public class mqtt_client
    {
        public mqtt_client()
        {

        }

        public void Connector()
        {
            Task.Run(async () => { await ConnectingToServer(); });
        }

        private async Task ConnectingToServer()
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("SinkClient")
                .WithTcpServer("localhost")
                .Build();

            await mqttClient.ConnectAsync(options);

            //Console.WriteLine("Press any key to exit.");
            //Console.ReadLine();

            //await mqttClient.DisconnectAsync();
        }

        public async Task Publish_Message(string server, string topic, string message)
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithClientId("SinkClient")
                    .WithTcpServer(server)
                    .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(message)
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

                await mqttClient.DisconnectAsync();

                Console.WriteLine("MQTT application message is published.");
            }
        }
    }
}
