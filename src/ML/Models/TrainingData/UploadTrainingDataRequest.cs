using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ML.Controllers.TrainingData
{
    public class UploadTrainingDataRequest : TrainingDataRequestBase
    {
        [Required]
        [Display(Name = "Training data files")]
        public IFormFile FormFile { get; set; }
    }
}
