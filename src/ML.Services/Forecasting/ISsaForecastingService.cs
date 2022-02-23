using ML.AiModels.Forecasting;

namespace ML.Services.Forecasting
{
    /// <summary>
    /// Interface for <class cref="SsaForecastingService"></class>
    /// </summary>
    public interface ISsaForecastingService
    {
        SsaForecastingOutput Create(CreateSsaForecastingDto options);
    }
}
