using System;
using System.Runtime.Serialization;

namespace GismeteoParser.Exceptions
{
    [Serializable]
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }
        public NotFoundException(string element) : base($"{element} not found") { }
        public NotFoundException(string element, Exception inner) : base($"{element} not found", inner) { }
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
