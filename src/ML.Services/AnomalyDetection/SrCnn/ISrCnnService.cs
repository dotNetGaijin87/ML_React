using ML.Models;
using System.Collections.Generic;

namespace ML.Services
{
    /// <summary>
    /// Interface for <class cref="SrCnnService"></class>
    /// </summary>
    public interface ISrCnnService
    {
        IEnumerable<SrCnnOutput> Predict(SrCnnOptions input);
    }
}