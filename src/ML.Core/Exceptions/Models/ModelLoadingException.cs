using System;

namespace ML.Core
{
    public class ModelLoadingException : Exception
    {
        public ModelLoadingException() { }
        public ModelLoadingException(string name)
            : base($"Model \"{name}\" loading error.") { }
        public ModelLoadingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
