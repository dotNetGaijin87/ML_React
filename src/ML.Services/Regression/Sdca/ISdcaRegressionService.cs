namespace ML.Services.Regression
{
    /// <summary>
    /// Interface for <class cref="SdcaRegressionService"></class>
    /// </summary>
    public interface ISdcaRegressionService : IRegressionServiceBase
    {
        public RegressionServiceCreateModelOutput Create(CreateSdcaRegressionModelOptions options);
    }
}
