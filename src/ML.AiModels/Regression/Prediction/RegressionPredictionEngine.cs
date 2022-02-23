using Microsoft.ML;
using ML.AiModels.Common;
using System.Collections.Generic;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Class used for calculating prediction of unknown data based known input data, and trained model
    /// </summary>
    public class RegressionPredictionEngine : IRegressionPredictionEngine
    {
        private MLContext _mlContext;
        private readonly string FEATURES = "Features";
        public RegressionPredictionEngine(MLContext mlContext)
        {
             _mlContext = mlContext;
        }

        /// <summary>
        /// Creates prediction engine from provided model, 
        /// and then runs it with input data to obtain score for the searched parameter
        /// </summary>
        /// <param name="mlModel">Model used for creating a prediction engine</param>
        /// <param name="predictionTarget">Input data that the prediction is based on</param>
        /// <returns></returns>
        public (RegressionPredictionOutput, List<string>) RunSingle(ITransformer mlModel, PredictionEngineInput predictionTarget)
        {
            var predEngine = _mlContext.Model.CreatePredictionEngine<PredictionEngineInput, RegressionPredictionOutput>(mlModel);
            RegressionPredictionOutput prediction = predEngine.Predict(predictionTarget);

            List<string> featureColumnNames = MlNetRegressionHelpers.TryGetFeatureColumnNames(predEngine.OutputSchema, FEATURES);

            return (prediction, featureColumnNames);
        }

        public List<RegressionPredictionOutput> RunMultiple(ITransformer mlModel, IEnumerable<PredictionEngineInput> predictionTarget)
        {
            var predEngine = _mlContext.Model.CreatePredictionEngine<PredictionEngineInput, RegressionPredictionOutput>(mlModel);
            var predictions = new List<RegressionPredictionOutput>();
            foreach (var singleInput in predictionTarget)
            {
                predictions.Add(predEngine.Predict(singleInput));
            }
 

            return predictions;
        }


        public List<string> GetModelColumnNames(ITransformer mlModel)
        {
            var predEngine = _mlContext.Model.CreatePredictionEngine<PredictionEngineInput, RegressionPredictionOutput>(mlModel);
    
            List<string> featureColumnNames = MlNetRegressionHelpers.TryGetFeatureColumnNames(predEngine.OutputSchema, FEATURES);

            return featureColumnNames;
        }

    }
}
