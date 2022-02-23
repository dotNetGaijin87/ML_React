using Microsoft.ML;
using ML.AiModels.Common;
using System.Collections.Generic;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Interface for <class cref="RegressionPredictionEngine"/> 
    /// </summary>
    public interface IRegressionPredictionEngine
    {
        (RegressionPredictionOutput, List<string>) RunSingle(ITransformer mlModel, PredictionEngineInput predictionTarget);

        List<RegressionPredictionOutput> RunMultiple(ITransformer mlModel, IEnumerable<PredictionEngineInput> predictionTarget);
        List<string> GetModelColumnNames(ITransformer mlModel);
    }
}