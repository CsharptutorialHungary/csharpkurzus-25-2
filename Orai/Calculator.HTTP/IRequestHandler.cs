using System.Net.Sockets;

namespace Calculator.HTTP;

public interface IRequestHandler
{
    Task<bool> HandlerRequest(HttpRequest request, NetworkStream responseStream, CancellationToken token);
}