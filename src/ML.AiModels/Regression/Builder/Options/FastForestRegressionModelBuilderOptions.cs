using Microsoft.ML.Trainers.FastTree;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Options used by the FastForest model builder
    /// </summary>
    public class FastForestRegressionModelBuilderOptions : RegressionModelBuilderOptionsBase
    {
        public FastForestRegressionTrainer.Options TrainerOptions { get; set; }
    }
}
