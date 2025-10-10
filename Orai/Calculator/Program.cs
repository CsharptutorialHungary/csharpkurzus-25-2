using Calculator.Core;

Console.WriteLine("Welcome to the RPN Calculator!");
Console.Write("> ");
string expression = Console.ReadLine() ?? string.Empty;

ICalculator calculator = CalculatorFactory.Create(infix: true);

try
{
    Result<double, string> result = calculator.Calculate(expression);
    Console.WriteLine(result);
}
catch (Exception ex)
{
    // Pokémon exception handling
    Console.WriteLine(ex.Message);
}
