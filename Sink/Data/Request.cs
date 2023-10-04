namespace Sink.Data
{
    internal class Request
    {
        public string Method { get; internal set; }
        public string Path { get; internal set; }
        public string Message { get; internal set; }
        public DateTime Time { get; internal set; }
    }
}