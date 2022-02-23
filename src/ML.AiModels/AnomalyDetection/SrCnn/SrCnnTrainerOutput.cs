using Microsoft.ML.Data;

namespace ML.AiModels
{
    /// <summary>
    /// Prediction scores returned by an anomaly detector
    /// </summary>
    public class SrCnnTrainerOutput
    {
        [VectorType]
        public double[] Prediction { get; set; }
    }
}
