using System;

namespace ML.Core
{
    public class TrainingDataAlreadyExistException : Exception
    {
        public TrainingDataAlreadyExistException() { }
        public TrainingDataAlreadyExistException(string name)
            : base($"Training data \"{name}\" already exists") { }
        public TrainingDataAlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
