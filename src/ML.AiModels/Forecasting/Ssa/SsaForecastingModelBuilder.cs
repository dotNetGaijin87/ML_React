using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using ML.AiModels.Common;

namespace ML.AiModels.Forecasting
{
    /// <summary>
    /// Ssa model builder
    /// </summary>
    public class SsaForecastingModelBuilder : ISsaForecastingModelBuilder
    {
        private MLContext _mlContext;

        public SsaForecastingModelBuilder(MLContext mlContext)
        {
            _mlContext = mlContext;
        }

        /// <summary>
        /// Uses <class cref="SsaForecastingTransformer"></class> transformer to run forecasting
        /// </summary>
        /// <param name="trainingData"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TimeSeriesPredictionEngine<PredictionEngineInput, SsaForecastingOutput> Build(IDataView trainingData, CreateSsaForecastingModel options)
        {
            SsaForecastingEstimator model = _mlContext.Forecasting.ForecastBySsa(
                                                outputColumnName: nameof(SsaForecastingOutput.Forecast),
                                                inputColumnName: options.InputColumnName,
                                                windowSize: options.WindowSize,
                                                seriesLength: options.SeriesLength,
                                                trainSize: options.TrainSize,
                                                horizon: options.Horizon,
                                                confidenceLevel: options.ConfidenceLevel,
                                                confidenceLowerBoundColumn: nameof(CreateSsaForecastingModel.LowerBound),
                                                confidenceUpperBoundColumn: nameof(CreateSsaForecastingModel.UpperBound));


            return model.Fit(trainingData)
                        .CreateTimeSeriesEngine<PredictionEngineInput, SsaForecastingOutput>(_mlContext);

        }

    }
}
