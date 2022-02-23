using ML.Core;

namespace ML.AiModels.Regression
{
    /// <summary>
    /// Base options used by the regression model builders
    /// </summary>
    public abstract class RegressionModelBuilderOptionsBase
    {
        public MlCategory Category { get; } = MlCategory.Regression;
        public string ModelName { get; set; }
        public string LabelColumnName { get; set; }
        public string[] FeatureColumnNames { get; set; }
        public int CrossValidationFoldsCount { get; set; }
        public string TrainingDataName { get; set; }
        public string FeaturesName { get; } = "Features";
        public bool HasFeatureContributionMetrics { get; set; } = true;
        public int PermutationCount { get; set; } = 20;
    }
}
