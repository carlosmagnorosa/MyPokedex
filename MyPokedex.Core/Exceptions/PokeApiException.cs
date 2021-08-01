using System;
using System.Runtime.Serialization;

namespace MyPokedex.Core.Exceptions
{
    [Serializable]
    public class PokeApiException : Exception
    {
        public PokeApiException()
        {
        }

        public PokeApiException(string message) : base(message)
        {
        }

        public PokeApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PokeApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}