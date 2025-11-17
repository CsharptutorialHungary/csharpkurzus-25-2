using System.Net.Sockets;
using System.Text;

namespace Calculator.HTTP;

internal static class SpecialHandlers
{
    internal static async Task HandleNotFound(NetworkStream stream)
    {
        const string message = "Not Found";

        string response = $"""
            HTTP/1.1 404 Not Found
            Date: {DateTime.UtcNow:R}
            Content-Type: text/plain; charset=utf-8
            Content-Length: {Encoding.UTF8.GetByteCount(message)}

            {message}
            """;

        using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        await writer.WriteAsync(response);
    }

    internal static async Task HandleServerError(NetworkStream stream, string message)
    {
        string response = $"""
            HTTP/1.1 500 Internal Server Error
            Date: {DateTime.UtcNow:R}
            Content-Type: text/plain; charset=utf-8
            Content-Length: {Encoding.UTF8.GetByteCount(message)}

            {message}
            """;

        using var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true);
        await writer.WriteAsync(response);
    }
}
