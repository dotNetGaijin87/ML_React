using Microsoft.ML;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Generic regression model builder interface
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public interface IModelBuilder<TOptions> where TOptions: RegressionModelBuilderOptionsBase
    {
        (ITransformer, RegressionModelBuilderOutput) Build(IDataView trainingData, TOptions options);
    }
}
