using System.ComponentModel.DataAnnotations;

namespace ML.Controllers.TrainingData
{
    public class SingleTrainingDataRequest : TrainingDataRequestBase
    {
        [Required]
        [Display(Name = "File name")]
        public string FileName { get; set; }
    }
}
