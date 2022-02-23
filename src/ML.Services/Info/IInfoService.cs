using System.Collections.Generic;

namespace ML.Services.Environment
{
    /// <summary>
    /// Interface for <class cref="InfoService"></class>
    /// </summary>
    public interface IInfoService
    {
        IEnumerable<string> ListCategories();
        Dictionary<string, IEnumerable<ModelTypeToUrl>> ListModels(string categoryName = null);
    }
}
