namespace Calculator.HTTP;

public interface ILogger
{
    void Warning(string message);
    void Error(string message);
    void Info(string message);
}
