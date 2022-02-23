namespace ML.AiModels.Regression
{
    /// <summary>
    ///  Output score of FastForest regression algorithm's prediction engine
    /// </summary>
    public class RegressionPredictionOutput
    {
        public float Score { get; set; }
        public float[] Features { get; set; }
        public float[] FeatureContributions { get; set; }
    }
}
