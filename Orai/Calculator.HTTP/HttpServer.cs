using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

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
