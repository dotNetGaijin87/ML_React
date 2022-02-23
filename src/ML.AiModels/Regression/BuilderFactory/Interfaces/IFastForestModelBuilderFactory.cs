using Microsoft.ML;

namespace ML.AiModels.Regression
{
    public interface IFastForestModelBuilderFactory
    {
        public IModelBuilder<FastForestRegressionModelBuilderOptions> CreateFastForestModelBuilder(MLContext mlContext);
    }
}
