using System.ComponentModel.DataAnnotations;

namespace ML.Controllers.TrainingData
{
    public class TrainingDataRequestBase
    {
        [Required]
        [Display(Name = "Category type")]
        public string ModelCategoryName { get; set; }

        [Required]
        [Display(Name = "Model type")]
        public string ModelTypeName { get; set; }

    }
}
