namespace GamblingSimulator.Core.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException()
            : base("Insufficient balance.") { }

        public InsufficientBalanceException(string message)
            : base(message) { }

        public InsufficientBalanceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
