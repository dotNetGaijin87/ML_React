using ML.Core;

namespace ML.Services
{
    /// <summary>
    /// Base model for forecasting services
    /// </summary>
    public abstract class ForecastingServiceOptions 
    {
        public MlCategory Category { get; } = MlCategory.Forecasting;
    }
}
