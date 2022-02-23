using System;

namespace ML.Core
{
    public class TrainingDataNotFoundException : Exception
    {
        public TrainingDataNotFoundException() { }
        public TrainingDataNotFoundException(string name) 
            : base($"Training data \"{name}\" was not found.") { }
        public TrainingDataNotFoundException(string message, Exception innerException) 
            : base(message, innerException) { }

    }
}
