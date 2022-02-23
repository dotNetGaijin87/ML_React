using System.ComponentModel.DataAnnotations;

namespace ML.Services.Regression
{
    public class CreateSdcaRegressionModelOptions : CreateRegressionDtoBase
    {
        [Required]
        [Display(Name = "Convergence tolerance")]
        public float ConvergenceTolerance { get; set; }

        [Required]
        [Display(Name = "Maximum number of iterations")]
        public int MaximumNumberOfIterations { get; set; }

        [Required]
        [Display(Name = "Bias learning rate")]
        public float BiasLearningRate { get; set; }
    }
}
