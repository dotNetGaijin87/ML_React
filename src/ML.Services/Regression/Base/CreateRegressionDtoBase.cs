using System;
using System.ComponentModel.DataAnnotations;

namespace ML.Services.Regression
{
    /// <summary>
    /// A Base class for requests for creating regression models
    /// </summary>
    public class CreateRegressionDtoBase
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        [Display(Name = "Model name")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Label column name")]
        public string LabelColumnName { get; set; }

        [Required]
        [Display(Name = "Feature column names")]
        public string[] FeatureColumnNames { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 3)]
        [Display(Name = "Training data file name")]
        public string TrainingDataName { get; set; }

        [Required]
        [Range(1, 40)]
        [Display(Name = "Cross validation folds count")]
        public int CrossValidationFoldsCount { get; set; }

        [Required]
        [Range(1, 200)]
        [Display(Name = "Permutation count")]
        public int PermutationCount { get; set; } = 20;

        public bool HasFeatureContributionMetrics { get; set; }

    }
}
