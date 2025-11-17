using Calculator.Core;
using Calculator.HTTP;

HttpServer server = new HttpServer(8080);
server.Start();
Console.ReadKey();
server.Stop();

//Console.WriteLine("Welcome to the RPN Calculator!");
//Console.Write("> ");
//string expression = Console.ReadLine() ?? string.Empty;

//ICalculator calculator = CalculatorFactory.Create();

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
