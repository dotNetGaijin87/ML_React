using ML.Common;
using System.Collections.Generic;

namespace ML.Services.Environment
{
    /// <summary>
    /// Service providing infromation on available ml algorithms and their categories
    /// </summary>
    public class InfoService : IInfoService
    {
        public Dictionary<string, IEnumerable<ModelTypeToUrl>> ListModels(string categoryName = null)
        {
            if(categoryName == null)
            {
                return MlModelToUrlMappings.CategoryToModelDictionary;
            }
            else
            {
                return new Dictionary<string, IEnumerable<ModelTypeToUrl>>
                {
                    [categoryName] = MlModelToUrlMappings.CategoryToModelDictionary[categoryName]
                };   
            }
        }

        public IEnumerable<string> ListCategories()
        {
            return MlModelToUrlMappings.CategoryToModelDictionary.Keys;
        }
    }
}
