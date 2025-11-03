namespace Calculator.HTTP;

public sealed class HttpRequest
{
    public Method Method { get; set; }
    public Uri Path { get; set; }
    public string Version { get; set; }
    public OrderedDictionary<string, string> Headers { get; set; }
    public string? Body { get; set; }

    public HttpRequest()
    {
        Headers = new OrderedDictionary<string, string>();
        Version = "HTTP/1.1";
        Path = new Uri("/", UriKind.Relative);
    }

    public long? Length
    {
        get
        {
            return Headers.TryGetValue("Content-Length", out string? value)
                ? long.Parse(value)
                : null;
        }
    }
}
