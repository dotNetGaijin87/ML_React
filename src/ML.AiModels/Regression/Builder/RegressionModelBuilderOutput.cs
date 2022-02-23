using Microsoft.ML.Data;
using System.Collections.Generic;
using static Microsoft.ML.TrainCatalogBase;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Performance metrics of regression ml model
    /// </summary>
    public class RegressionModelBuilderOutput
    {
        public IEnumerable<CrossValidationResult<RegressionMetrics>> ValidationResults { get; set; }
            = new List<CrossValidationResult<RegressionMetrics>>();
        public IEnumerable<RegressionMetricsStatistics> FeatureImportanceList { get; set; } 
            = new List<RegressionMetricsStatistics>();
        public IEnumerable<string> ContributingFeatureIndexes { get; set; } = new List<string>();
    }
}
