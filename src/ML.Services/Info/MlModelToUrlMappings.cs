using System.Collections.Generic;

namespace ML.Services.Environment
{
    /// <summary>
    /// Stores ml model type names and their corresponding urls
    /// </summary>
    public static class MlModelToUrlMappings
    {
        public static Dictionary<string, IEnumerable<ModelTypeToUrl>> CategoryToModelDictionary { get; set; } = new Dictionary<string, IEnumerable<ModelTypeToUrl>>
        {
            ["regression"] = new List<ModelTypeToUrl>
                            { 
                                new ModelTypeToUrl("FastForest", "fast-forest"),
                                new ModelTypeToUrl("FastTree", "fast-tree"),
                                new ModelTypeToUrl("Sdca", "sdca") 
                            },
            ["forecasting"] = new List<ModelTypeToUrl>
                            {
                                new ModelTypeToUrl("Ssa", "ssa"),
    
                            }
        };
    }

    public struct ModelTypeToUrl
    {
        public ModelTypeToUrl(string modelType, string url)
        {
            ModelType = modelType;
            Url = url;
        }
        public readonly string ModelType { get; }
        public readonly string Url { get; }
    }
}
