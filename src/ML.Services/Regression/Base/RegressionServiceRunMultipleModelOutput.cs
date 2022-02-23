using System.Collections.Generic;

namespace ML.Services.Regression
{
    /// <summary>
    /// Scores for running a regression model on multiple data rows
    /// </summary>
    public class RegressionServiceRunMultipleModelOutput
    {
        public List<float> Scores { get; init; }
    }
}
