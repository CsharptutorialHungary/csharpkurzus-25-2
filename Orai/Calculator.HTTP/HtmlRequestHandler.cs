using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.HTTP;

public class HtmlRequestHandler : IRequestHandler
{
    private readonly string _htmlContent;
    private readonly string _serverPath;

    public HtmlRequestHandler(string serverPath, string localPath)
    {
        if (!File.Exists(localPath))
            throw new FileNotFoundException($"{localPath} doesn't exist");

        _htmlContent = File.ReadAllText(localPath);
        _serverPath = serverPath;
    }

    public async Task<bool> HandlerRequest(HttpRequest request, NetworkStream responseStream, CancellationToken token)
    {
        if (request.Path.LocalPath != _serverPath)
            return false;

        string response = $"""
            HTTP/1.1 200 Ok
            Date: {DateTime.UtcNow:R}
            Content-Type: text/html; charset=utf-8
            Content-Length: {Encoding.UTF8.GetByteCount(_htmlContent)}

            {_htmlContent}
            """;

        using var writer = new StreamWriter(responseStream, Encoding.UTF8, leaveOpen: true);
        await writer.WriteAsync(response);

        return true;
    }
}
