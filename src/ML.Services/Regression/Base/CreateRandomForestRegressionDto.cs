using System;
using System.ComponentModel.DataAnnotations;

namespace ML.Services.Regression
{
    public class CreateRandomForestRegressionDto : CreateRegressionDtoBase
    {
        [Required]
        [Range(2, 1000)]
        [Display(Name = "Leaves count")]
        public int LeavesCount { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Trees count")]
        public int TreesCount { get; set; }

        [Required]
        [Range(2, 1000)]
        [Display(Name = "Minimum example count per leaf")]
        public int MinimumExampleCountPerLeaf { get; set; }
    }
}
