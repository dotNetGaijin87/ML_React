using System;

namespace ML.Core
{
    public class SavingTrainingDataException : Exception
    {
        public SavingTrainingDataException() { }
        public SavingTrainingDataException(string name)
            : base($"Saving \"{name}\" data error.") { }
        public SavingTrainingDataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
