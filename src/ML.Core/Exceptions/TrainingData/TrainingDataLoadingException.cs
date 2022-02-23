using System;

namespace ML.Core
{
    public class TrainingDataLoadingException : Exception
    {
        public TrainingDataLoadingException() { }
        public TrainingDataLoadingException(string name)
            : base($"Training data \"{name}\" loading error.") { }
        public TrainingDataLoadingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
