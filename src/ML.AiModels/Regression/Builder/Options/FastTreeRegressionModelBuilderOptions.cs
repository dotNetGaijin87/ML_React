using Microsoft.ML.Trainers.FastTree;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Options used by the FastTree model builder
    /// </summary>
    public class FastTreeRegressionModelBuilderOptions : RegressionModelBuilderOptionsBase
    {
        public FastTreeRegressionTrainer.Options TrainerOptions { get; set; }
    }
}
