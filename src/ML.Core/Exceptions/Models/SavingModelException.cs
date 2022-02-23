using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core
{
    public class SavingModelException : Exception
    {
        public SavingModelException() { }
        public SavingModelException(string name)
            : base($"Saving \"{name}\" model error.") { }
        public SavingModelException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
