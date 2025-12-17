using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ACeg.CalculatorClient;

internal class CachingCalculatorClient : ICalculatorClient
{
    private readonly ICalculatorClient _calculatorClient;
    private readonly TimeProvider _timeProvider;
    private readonly ConcurrentDictionary<string, (DateTimeOffset lastAcces, string value)> _cache;

    public CachingCalculatorClient(ICalculatorClient calculatorClient, TimeProvider timeProvider)
    {
        _calculatorClient = calculatorClient;
        _timeProvider = timeProvider;
        _cache = new ConcurrentDictionary<string, (DateTimeOffset lastAcces, string value)>();
    }

    public async ValueTask<string> CalculateAsync(string expression, CancellationToken token = default)
    {
        if (_cache.TryGetValue(expression, out (DateTimeOffset lastAcces, string value) item))
        {
            item.lastAcces = _timeProvider.GetUtcNow();
            _cache[expression] = item;
            return item.value;
        }

        string result = await _calculatorClient.CalculateAsync(expression, token);

        _cache.TryAdd(expression, (_timeProvider.GetUtcNow(), result));
        CleanupOldItems();
        return result;
    }

    private void CleanupOldItems()
    {
        if (_cache.Count > 100)
        {
            var oldest = _cache.OrderBy(item => item.Value.lastAcces).First();
            _cache.TryRemove(oldest);
        }
    }
}

internal class CalculatorClient : ICalculatorClient
{
    public CalculatorClient(string url)
    {
        _url = url;
    }

    private readonly string _url;

    public async ValueTask<string> CalculateAsync(string expression, CancellationToken token = default)
    {
        using HttpClient client = new HttpClient();

        string encoded = HttpUtility.UrlEncode(expression);
        string url = $"{_url}?expression={encoded}";
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string output = await response.Content.ReadAsStringAsync();
            return output;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
