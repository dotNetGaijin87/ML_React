using ML.AiModels.Regression;
using System.Collections.Generic;

namespace ML.Services.Regression
{
    /// <summary>
    /// Interface for <class cref="RegressionServiceBase"></class>
    /// </summary>
    public interface IRegressionServiceBase
    {
        RegressionServiceCreateModelOutput CreateModel<TOptions>(IModelBuilder<TOptions> modelBuilder, TOptions options)
               where TOptions : RegressionModelBuilderOptionsBase;
        bool Delete(string modelName);
        IEnumerable<string> List();
        RegressionServiceRunSingleModelOutput RunSingle(string modelName, string[] data);
        RegressionServiceRunMultipleModelOutput RunMultiple(string modelName, string[][] data);
    }
}