using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.AiModels
{
    public static class MlNetRegressionHelpers
    {
        /// <summary>
        /// Helper function for retrieving feature column names from ML.NET models
        /// The solution is based on "https://github.com/dotnet/machinelearning/issues/1881"
        /// </summary>
        /// <param name="schema">Model schema</param>
        /// <param name="featureColumnName"></param>
        /// <returns></returns>
        public static List<string> TryGetFeatureColumnNames(DataViewSchema schema, string featureColumnName)
        {
            var slotNames = new VBuffer<ReadOnlyMemory<char>>();
            var column = schema.GetColumnOrNull(featureColumnName);
            column.Value.GetSlotNames(ref slotNames);
            var names = new string[slotNames.Length];
            var num = 0;
            foreach (var denseValue in slotNames.DenseValues())
            {
                names[num++] = denseValue.ToString();
            }

            return names.ToList();
        }
    }
}
