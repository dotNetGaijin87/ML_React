namespace ML.Services.Regression
{
    /// <summary>
    /// Interface for <class cref="FastTreeRegressionService"></class>
    /// </summary>
    public interface IFastTreeRegressionService : IRegressionServiceBase
    {
        public RegressionServiceCreateModelOutput Create(CreateFastTreeRegressionModelOptions options);
    }
}
