using System;

namespace ML.Core
{
    public class MultipleModelsWithSameNameFoundException : Exception
    {
        public MultipleModelsWithSameNameFoundException() { }
        public MultipleModelsWithSameNameFoundException(string name)
            : base($"Multiple files with the same name \"{name}\" found.") { }
        public MultipleModelsWithSameNameFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
