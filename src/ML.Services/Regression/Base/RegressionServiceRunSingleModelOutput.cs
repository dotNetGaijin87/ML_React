using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Services.Regression
{
    /// <summary>
    /// Model characteristics a prediction (score) for running a regression model on a single data row
    /// </summary>
    public class RegressionServiceRunSingleModelOutput
    {
        public float Score { get; set; }
        public float[] Features { get; set; }
        public float[] FeatureContributions { get; set; }
        public List<int> ContributingFeatureIndexes { get; set; }
    }
}
