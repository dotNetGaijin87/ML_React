using System;

namespace ML.Core
{
    public class ModelNotFoundException : Exception
    {
        public ModelNotFoundException() { }
        public ModelNotFoundException(string name)
            : base($"Model \"{name}\" not found.") { }
        public ModelNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
