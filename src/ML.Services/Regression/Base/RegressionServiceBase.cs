using Microsoft.ML;
using ML.AiModels.Common;
using ML.AiModels.Regression;
using ML.Common.MlRepository;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ML.Services.Regression
{
    /// <summary>
    /// Base common class for regression services
    /// </summary>
    public class RegressionServiceBase : IRegressionServiceBase
    {
        protected readonly IRegressionPredictionEngine _predictionEngine;
        protected readonly IMlRepository _mLRepository;
        protected MlAlgorithm ModelType { get; init; }
        protected MlCategory ModelCategory { get; init; }



        public RegressionServiceBase(IRegressionPredictionEngine predictionEngineBuilder, IMlRepository mLDataLoader)
        {
            _predictionEngine = predictionEngineBuilder;
            _mLRepository = mLDataLoader;
        }

        /// <summary>
        /// Creates a ml model
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public RegressionServiceCreateModelOutput CreateModel<TOptions>(IModelBuilder<TOptions> modelBuilder, TOptions options)
            where TOptions : RegressionModelBuilderOptionsBase
        {

            var loadDataDto = new TrainingDataModel
            {
                Category = options.Category,
                AlgorithmType = ModelType,
                Name = options.TrainingDataName,
            };

            IDataView trainingData = _mLRepository.LoadTrainingData<PredictionEngineInput>(loadDataDto);


            (ITransformer mlModel, RegressionModelBuilderOutput modelBuilderOutput) = modelBuilder.Build(trainingData, options);

            // Saves the model
            var saveModelDto = new SaveMlNetModelDto()
            {
                AiModel = mlModel,
                Category = options.Category,
                Type = ModelType,
                Name = options.ModelName,
                Schema = trainingData.Schema,
            };

            _mLRepository.SaveModel(saveModelDto);


            return new RegressionServiceCreateModelOutput
            {
                FeatureImportanceList = modelBuilderOutput.FeatureImportanceList.Select(x => x.RSquared.Mean).ToList(),
                ValidationResults = modelBuilderOutput.ValidationResults.Select(x => x.Metrics.RSquared).ToList(),
                ContributingFeatureIndexes = GetFeatureColumnIndexes(modelBuilderOutput.ContributingFeatureIndexes)
            };
        }

        /// <summary>
        /// Deletes ml model
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public bool Delete(string modelName)
        {
            var deleteModel = new SaveMlNetModelDto
            {
                Category = ModelCategory,
                Type = ModelType,
                 Name = modelName
            };

            return _mLRepository.DeleteModel(deleteModel);
        }

        /// <summary>
        /// Lists ml model names
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> List()
        {
            var listModel = new Entity
            {
                Category = ModelCategory,
                AlgorithmType = ModelType,
            };

            return _mLRepository.ListModels(listModel);
        }

        /// <summary>
        /// Calculates a single prediction for a single data row
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegressionServiceRunSingleModelOutput RunSingle(string modelName, string[] data)
        {
            var loadModel = new MlNetModel
            {
                Category = ModelCategory,
                Type = ModelType,
                Name = modelName
            };

            ITransformer model = _mLRepository.LoadModel(loadModel);
            var dataModel = new PredictionEngineInput();
            PropertyInfo[] dataModelProps = typeof(PredictionEngineInput).GetProperties();


            for (int i = 0; i < data.Length; i++)
            {
                if (Single.TryParse(data[i], out float result))
                {
                    dataModelProps[i].SetValue(dataModel, result, null);
                }
            }

            (RegressionPredictionOutput predictionOutput, List<string> featureColumnNames) = _predictionEngine.RunSingle(model, dataModel);


            return new RegressionServiceRunSingleModelOutput
            {
                ContributingFeatureIndexes = GetFeatureColumnIndexes(featureColumnNames),
                FeatureContributions = predictionOutput.FeatureContributions,
                Features = predictionOutput.Features,
                Score = predictionOutput.Score,
            };
        }

        /// <summary>
        /// Calculates multiple predictions for multiple data rows
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RegressionServiceRunMultipleModelOutput RunMultiple(string modelName, string[][] data)
        {
            var loadModel = new MlNetModel
            {
                Category = ModelCategory,
                Type = ModelType,
                Name = modelName
            };

            ITransformer model = _mLRepository.LoadModel(loadModel);
            var dataModel = new List<PredictionEngineInput>();
            PropertyInfo[] dataModelProps = typeof(PredictionEngineInput).GetProperties();
            var featureNames = _predictionEngine.GetModelColumnNames(model);
            var featureIndexes = GetFeatureColumnIndexes(featureNames);

            for (int i = 0; i < data.Count(); i++)
            {
                var singleData = new PredictionEngineInput();
                for (int j = 0; j < featureIndexes.Count(); j++)
                {
                    int featureIndex = featureIndexes[j];
                    dataModelProps[featureIndex].SetValue(singleData, Single.Parse(data[i][featureIndex]), null);
                }

                dataModel.Add(singleData);
            }
 
            return new RegressionServiceRunMultipleModelOutput 
            { 
                Scores = _predictionEngine.RunMultiple(model, dataModel)
                                            .Select(x => x.Score)
                                            .ToList() 
            };
        }
 
        /// <summary>
        /// Helper function for extracting indices from feature column names.
        /// Colum names follow a convention "ColumnXX", where XX is the column's index.
        /// </summary>
        /// <param name="columnList"></param>
        /// <returns></returns>
        private List<int> GetFeatureColumnIndexes(IEnumerable<string> columnList)
        {
            return columnList.Select(x => Int32.Parse(x.Remove(0, "Column".Length))).ToList();
        }
    }
}
