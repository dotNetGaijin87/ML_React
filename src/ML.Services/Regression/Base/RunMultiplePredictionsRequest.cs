using System.ComponentModel.DataAnnotations;

namespace ML.Services.Regression
{
    public class RunMultiplePredictionsRequest
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        [Display(Name = "Model name")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Data for prediction")]
        public string[][] Data { get; set; }              
    }
}
