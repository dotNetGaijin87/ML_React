using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ML.AiModels
{
    /// <summary>
    /// Time series data that is fed into an anomaly detector
    /// </summary>
    public class SrCnnTrainerInputCollection
    {
        [Required]
        [Range(12, int.MaxValue)]
        public IEnumerable<SrCnnTrainerInput> Values { get; set; }
    }

    /// <summary>
    /// Helper class for wrapping the data
    /// </summary>
    public class SrCnnTrainerInput
    {
        public double Value { get; set; }
    }
}
