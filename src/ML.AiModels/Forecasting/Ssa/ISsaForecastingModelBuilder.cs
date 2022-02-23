using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using ML.AiModels.Common;

namespace ML.AiModels.Forecasting
{
    /// <summary>
    /// Interface for <class cref="SsaForecastingModelBuilder"></class>
    /// </summary>
    public interface ISsaForecastingModelBuilder
    {
        TimeSeriesPredictionEngine<PredictionEngineInput, SsaForecastingOutput> Build(IDataView trainingData, CreateSsaForecastingModel options);
    }
}