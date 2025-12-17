using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Web;

using Calculator;
using Calculator.Core;
using Calculator.HTTP;

var frontend = Path.Combine(AppContext.BaseDirectory, "frontend.html");

var logger = new ConsoleLogger();

HttpServer server = new HttpServer(8080, logger, new HtmlRequestHandler("/", frontend), new CalculateHandler());
server.Start();
Console.ReadKey();
server.Stop();

internal class CalculateHandler : JsonRequestHandler<string>
{
    public CalculateHandler() : base("/evaluate")
    {
    }

    public override Task<(string response, HttpStatusCode code)> Handle(HttpRequest httpRequest)
    {
        var query = HttpUtility.ParseQueryString(httpRequest.Path.Query);
        string? expression = query["expression"];
        if (string.IsNullOrWhiteSpace(expression))
        {
            return Task.FromResult((string.Empty, HttpStatusCode.BadRequest));
        }

        ICalculator calculator = CalculatorFactory.Create();
        var result = calculator.Calculate(expression);

        (string content, HttpStatusCode code) toReturn = default!;

        result.Visit((okResult) => toReturn = (okResult.ToString(CultureInfo.InvariantCulture), HttpStatusCode.OK),
                     (error) => toReturn = (error, HttpStatusCode.BadRequest));

        return Task.FromResult(toReturn);
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
