using System;

namespace ML.Core
{
    public class DeletingTrainingDataException : Exception
    {
        public DeletingTrainingDataException() { }
        public DeletingTrainingDataException(string name)
            : base($"Deleting \"{name}\" training data error.") { }
        public DeletingTrainingDataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
