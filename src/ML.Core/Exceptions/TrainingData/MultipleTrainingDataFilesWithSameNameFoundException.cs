using System;

namespace ML.Core
{
    public class MultipleTrainingDataFilesWithSameNameFoundException : Exception
    {
        public MultipleTrainingDataFilesWithSameNameFoundException() { }
        public MultipleTrainingDataFilesWithSameNameFoundException(string name)
            : base($"Multiple files with the same name \"{name}\" found.") { }
        public MultipleTrainingDataFilesWithSameNameFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
