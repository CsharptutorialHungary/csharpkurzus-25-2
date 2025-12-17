using System.Web;

namespace ACeg.CalculatorClient;

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
