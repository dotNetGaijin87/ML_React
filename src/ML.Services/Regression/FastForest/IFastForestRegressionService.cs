namespace ML.Services.Regression
{
    /// <summary>
    /// Interface for <class cref="FastForestRegressionService"></class>
    /// </summary>
    public interface IFastForestRegressionService : IRegressionServiceBase
    {
        public RegressionServiceCreateModelOutput Create(CreateFastForestRegressionDto options);
    }
}
