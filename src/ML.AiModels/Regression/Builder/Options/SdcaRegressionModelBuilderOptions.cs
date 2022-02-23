using Microsoft.ML.Trainers;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Options used by the Sdca model builder
    /// </summary>
    public class SdcaRegressionModelBuilderOptions : RegressionModelBuilderOptionsBase
    {
        public SdcaRegressionTrainer.Options TrainerOptions { get; set; }
    }
}
