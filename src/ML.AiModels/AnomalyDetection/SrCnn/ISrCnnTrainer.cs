using Microsoft.ML.TimeSeries;
using System.Collections.Generic;

namespace ML.AiModels
{
    /// <summary>
    /// Interface for <class cref="SrCnnTrainer"></class>
    /// </summary>
    public interface ISrCnnTrainer
    {
        IEnumerable<SrCnnTrainerOutput> Run(SrCnnTrainerInputCollection input, SrCnnEntireAnomalyDetectorOptions options);
    }
}