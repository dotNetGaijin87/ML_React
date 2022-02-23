namespace ML.AiModels.Forecasting
{
    /// <summary>
    /// Options for configuring the forecasting algorithm
    /// </summary>
    public class CreateSsaForecastingModel
    {
        public string TrainingDataName { get; set; }

        public string InputColumnName { get; set; }

        public float LowerBound { get; set; }

        public float UpperBound { get; set; }

        public int WindowSize { get; set; }

        public int SeriesLength { get; set; }

        public int TrainSize { get; set; }

        public int Horizon { get; set; }

        public float ConfidenceLevel { get; set; }

    }
}
