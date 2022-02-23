using System;

namespace ML.Core.Utils
{
    /// <summary>
    /// Utility functions for converting strings to its enum representations
    /// </summary>
    public static class Converters
    {
        public static MlCategory GetCategoryFromString(string data)
        {
            return (MlCategory)Enum.Parse(typeof(MlCategory), data, true);
        }

        public static MlAlgorithm GetAiAlgorithmFromString(string data)
        {
            return (MlAlgorithm)Enum.Parse(typeof(MlAlgorithm), data, true);
        }

        public static EntityType GetEntityTypeFromString(string data)
        {
            return (EntityType)Enum.Parse(typeof(EntityType), data, true);
        }
    }
}
