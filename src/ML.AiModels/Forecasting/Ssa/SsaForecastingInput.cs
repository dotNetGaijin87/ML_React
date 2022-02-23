using Microsoft.ML.Data;

namespace ML.AiModels.Forecasting
{
    /// <summary>
    /// Input data used for running the forecasting algorithm
    /// </summary>
    public class SsaForecastingInput
    {
        [ColumnName("Value"), LoadColumn(0)]
        public float Value { get; set; }

    }
}
