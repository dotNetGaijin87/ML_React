using Microsoft.ML;
using ML.Core;

namespace ML.Common.MlRepository
{
    public class MlNetModel
    {
        public MlCategory Category { get; init; }
        public MlAlgorithm Type { get; init; }
        public string Name { get; init; }
        public string Extension { get; } = "zip";
    }
}
