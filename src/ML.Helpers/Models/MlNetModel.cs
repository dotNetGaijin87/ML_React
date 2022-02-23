using Microsoft.ML;
using ML.Core;

namespace ML.Helpers.Models
{
    public class MlNetModel
    {
        public AiCategory Category { get; init; }
        public AiModelType Type { get; init; }
        public string Name { get; init; }
        public string Extension { get; } = "zip";
    }
}
