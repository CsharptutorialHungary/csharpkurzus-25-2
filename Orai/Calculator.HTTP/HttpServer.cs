using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.HTTP;

public class HttpServer : IDisposable
{
    private readonly int _port;
    private readonly TcpListener _listener;
    private readonly SemaphoreSlim _semaphore;
    private readonly CancellationTokenSource _cancellationTokenSource;

    private bool _disposed;

    public HttpServer(int port)
    {
        _port = port;
        _listener = new TcpListener(IPAddress.Any, port);
        _semaphore = new SemaphoreSlim(10);
        _cancellationTokenSource = new CancellationTokenSource();
    }

    ~HttpServer()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool isDirectCall)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(HttpServer));
        }

        _listener.Dispose();
        _semaphore.Dispose();
        _cancellationTokenSource.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        if (!_disposed)
        {
            _listener.Start();
            Task.Run(ListenTask, _cancellationTokenSource.Token);
        }
        else
        {
            throw new ObjectDisposedException(nameof(HttpServer));
        }
    }

    public void Stop()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        _cancellationTokenSource.Cancel();
        _listener.Stop();
    }

    private async Task ListenTask()
    {
        try
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                var client = await _listener.AcceptTcpClientAsync(_cancellationTokenSource.Token);
                await _semaphore.WaitAsync(_cancellationTokenSource.Token);
                try
                {
                    await HandleClient(client, _cancellationTokenSource.Token);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }
        catch (OperationCanceledException ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private async Task HandleClient(TcpClient client, CancellationToken cancellationToken)
    {
        using var stream = client.GetStream();
        try
        {
            HttpRequest request = await HttpRequestParser.ParseAsync(stream, _port);

            await SpecialHandlers.HandleNotFound(stream);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await SpecialHandlers.HandleServerError(stream, ex.Message);
        }
    }
}

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

public interface IRequestHandler
{
    Task<bool> HandlerRequest(HttpRequest request, NetworkStream responseStream, CancellationToken token);
}