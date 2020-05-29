using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManipulation.Exceptions
{

    [Serializable]
    public class InvalidArtDimensionsException : Exception
    {
        public InvalidArtDimensionsException() { }
        public InvalidArtDimensionsException(string message) : base(message) { }
        public InvalidArtDimensionsException(string message, Exception inner) : base(message, inner) { }
        protected InvalidArtDimensionsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
