using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using ML.AiModels.Common;
using ML.AiModels.Forecasting;
using ML.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ML.Services.Forecasting
{
    public class CreateSsaForecastingDto
    {
        public MlCategory Category { get; } = MlCategory.Forecasting;

        [Required]
        [StringLength(70, MinimumLength = 3)]
        [Display(Name = "Model name")]
        public string  ModelName { get; set; }

        [Required]
        [StringLength(70, MinimumLength = 3)]
        [Display(Name = "Training data file name")]
        public string TrainingDataName { get; set; }

        [Required]
        [Display(Name = "Input Column Name")]
        public string InputColumnName { get; set; }

        [Required]
        [Display(Name = "Lowerbound")]
        public float LowerBound { get; set; }

        [Required]
        [Display(Name = "Upperbound")]
        public float UpperBound { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Window size")]
        public int WindowSize { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Series length")]
        public int SeriesLength { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Train data size")]
        public int TrainSize { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Horizon")]
        public int Horizon { get; set; }

        [Required]
        [Range(0, 1.0)]
        [Display(Name = "Confidence level")]
        public float ConfidenceLevel { get; set; }

        public Action<MLContext, TimeSeriesPredictionEngine<PredictionEngineInput, SsaForecastingOutput>, string, string>
            SaveModel = (mlContext, engine, path, modelName)
                        => engine.CheckPoint(mlContext, Path.Combine(path, $"{modelName}.zip"));
    }
}
