namespace Sink
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await mqtt_server.Run_Server_With_Logging();
        }
    }
}
