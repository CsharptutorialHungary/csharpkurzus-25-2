using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.HTTP;

public class HttpServer : IDisposable
{
    private readonly TcpListener _listener;
    private bool _disposed;

    public HttpServer(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
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
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }



}
