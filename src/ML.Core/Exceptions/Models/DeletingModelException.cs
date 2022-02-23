using System;

namespace ML.Core
{
    public class DeletingModelException : Exception
    {
        public DeletingModelException() { }
        public DeletingModelException(string name)
            : base($"Deleting \"{name}\" model error.") { }
        public DeletingModelException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
