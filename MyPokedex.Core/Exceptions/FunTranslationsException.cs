using System;
using System.Runtime.Serialization;

namespace MyPokedex.Core.Exceptions
{
    [Serializable]
    public class FunTranslationsException : Exception
    {
        public FunTranslationsException()
        {
        }

        public FunTranslationsException(string message) : base(message)
        {
        }

        public FunTranslationsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FunTranslationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}