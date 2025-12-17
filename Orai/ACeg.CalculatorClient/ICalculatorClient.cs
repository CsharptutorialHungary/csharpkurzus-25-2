
namespace ACeg.CalculatorClient;

internal interface ICalculatorClient
{
    ValueTask<string> CalculateAsync(string expression, CancellationToken token = default);
}