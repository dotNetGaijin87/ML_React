using Microsoft.ML;

namespace ML.AiModels.Regression
{
    public interface ISdcaModelBuilderFactory
    {
        public IModelBuilder<SdcaRegressionModelBuilderOptions> CreateSdcaModelBuilder(MLContext mlContext);
    }
}
