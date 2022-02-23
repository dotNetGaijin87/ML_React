using Microsoft.ML;

namespace ML.AiModels.Regression
{
    public interface IFastTreeModelBuilderFactory
    {
        public IModelBuilder<FastTreeRegressionModelBuilderOptions> CreateFastTreeModelBuilder(MLContext mlContext);
    }
}
