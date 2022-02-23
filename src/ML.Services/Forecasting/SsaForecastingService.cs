using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using ML.AiModels.Common;
using ML.AiModels.Forecasting;
using ML.Common.DataPathRegister;
using ML.Common.MlRepository;
using ML.Core;
using System.IO;
using System.Linq;

namespace ML.Services.Forecasting
{
    /// <summary>
    /// Service for managing creation and using Ssa algorithm
    /// </summary>
    public class SsaForecastingService : ForecastingServiceOptions, ISsaForecastingService
    {
        private readonly ISsaForecastingModelBuilder _ssaForecastingModelBuilder;
        private readonly IMlRepository _mLRepository;
        private readonly IDataPathRegister _dataPathRegister;
        protected MlAlgorithm ModelType { get; init; }
        private MLContext _mlContext;

        public SsaForecastingService(
            ISsaForecastingModelBuilder ssaForecastingModel, 
            IMlRepository mLDataLoader,
            IDataPathRegister dataPathRegister,
            MLContext mlContext)
        {
            _ssaForecastingModelBuilder = ssaForecastingModel;
            _mLRepository = mLDataLoader;
            _dataPathRegister = dataPathRegister;
            _mlContext = mlContext;
            ModelType = MlAlgorithm.Ssa;
        }

        /// <summary>
        /// Creates Ssa forecasting model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SsaForecastingOutput Create(CreateSsaForecastingDto model)
        {
            var builderOptions = new CreateSsaForecastingModel
            {
                TrainingDataName = model.TrainingDataName,
                InputColumnName = GetHeadersFromFile(model.TrainingDataName, model.InputColumnName),
                LowerBound = model.LowerBound,
                UpperBound = model.UpperBound,
                WindowSize = model.WindowSize,
                SeriesLength = model.SeriesLength,
                TrainSize = model.TrainSize,
                Horizon = model.Horizon,
                ConfidenceLevel = model.ConfidenceLevel
            };

            var loadDataDto = new TrainingDataModel
            {
                Category = model.Category,
                AlgorithmType = ModelType,
                Name = model.TrainingDataName
            };

            IDataView trainingData = _mLRepository.LoadTrainingData<PredictionEngineInput>(loadDataDto);
            TimeSeriesPredictionEngine<PredictionEngineInput, SsaForecastingOutput> predictionEngine 
                = _ssaForecastingModelBuilder.Build(trainingData, builderOptions);

            string path = _dataPathRegister.GetModelFolderaPath(Category, ModelType);
            model.SaveModel(_mlContext, predictionEngine, path, model.ModelName);
 
            SsaForecastingOutput prediction = predictionEngine.Predict();


            return prediction;
        }

        private string GetHeadersFromFile(string fileName, string inputColumnName)
        {
            var trainingFilePath =  _dataPathRegister.GetTrainingDataFilePath(Category, ModelType, fileName);
            int indexedColumnName = -1;
            using (StreamReader sr = new StreamReader(trainingFilePath))
            {
                indexedColumnName = sr.ReadLine()
                                        .Split(',')
                                        .Select((value, index) => new { value, index })
                                        .Where(x => x.value == inputColumnName)
                                        .Select(x => x.index)
                                        .Single();
            }

            return "Column" + indexedColumnName;
        }
    }
}
