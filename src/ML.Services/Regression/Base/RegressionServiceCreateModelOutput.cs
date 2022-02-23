using System.Collections.Generic;

namespace ML.Services.Regression
{
    /// <summary>
    /// Performance metrics of a newly created regression model
    /// </summary>
    public class RegressionServiceCreateModelOutput
    {
        public List<double> FeatureImportanceList { get; set; } = new List<double>();
        public List<double> ValidationResults { get; set; } = new List<double>();
        public List<int> ContributingFeatureIndexes { get; set; } = new List<int>();
    }
}
