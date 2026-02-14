using System.Runtime.Serialization;

namespace OnePieceCrewManager.Core.Exceptions
{
    public class CrewException : Exception
    {
        public CrewException()
        {
        }

        public CrewException(string? message) : base(message)
        {
        }

        public CrewException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CrewException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
