using System.Web;

using Calculator.Core;
using Calculator.HTTP;

var frontend = Path.Combine(AppContext.BaseDirectory, "frontend.html");

HttpServer server = new HttpServer(8080, new HtmlRequestHandler("/", frontend), new CalculateHandler());
server.Start();
Console.ReadKey();
server.Stop();

internal class CalculateHandler : JsonRequestHandler<string>
{
    public CalculateHandler() : base("/evaluate")
    {
    }

    public override Task<string> Handle(HttpRequest httpRequest)
    {
        var query = HttpUtility.ParseQueryString(httpRequest.Path.Query);
        string? expression = query["expression"];
        if (string.IsNullOrWhiteSpace(expression))
        {
            return Task.FromResult("");
        }

        ICalculator calculator = CalculatorFactory.Create();
        var result = calculator.Calculate(expression);

        return Task.FromResult(result.ToString());
    }
}

//Console.WriteLine("Welcome to the RPN Calculator!");
//Console.Write("> ");
//string expression = Console.ReadLine() ?? string.Empty;

//

//try
//{
//    Result<double, string> result = calculator.Calculate(expression);
//    Console.WriteLine(result);
//}
//catch (Exception ex)
//{
//    // Pokémon exception handling
//    Console.WriteLine(ex.Message);
//}
