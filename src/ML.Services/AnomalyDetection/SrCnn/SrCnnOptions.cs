using Microsoft.ML.TimeSeries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ML.Models
{
    /// <summary>
    /// Input and options data used for running the SrCnn algorithm
    /// </summary>
    public class SrCnnOptions
    {
        [Required]
        [Range(0, 1)]
        public double Threshold { get; set; }

        [Required]
        public int BatchSize { get; set; }

        [Required]
        [Range(0, 100)]
        public double Sensitivity { get; set; }

        [Required]
        public int Period { get; set; }

        [Required]
        public List<double> TrainingData { get; set; }

    }
}
