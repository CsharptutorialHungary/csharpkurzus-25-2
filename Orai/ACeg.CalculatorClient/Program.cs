using System.Web;

using ACeg.CalculatorClient;

//TODO: Fix later
const string ServerUrl = "http://localhost:8080/evaluate";

var client = new CachingCalculatorClient(new CalculatorClient(ServerUrl), TimeProvider.System);

while (true)
{
    Console.Write("$>");
    string? expression = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(expression))
    {
       string result = await client.CalculateAsync(expression);
       Console.WriteLine(result);
    }
}