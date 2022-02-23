using Microsoft.ML;
using ML.Core;

namespace ML.Helpers.Models
{
    public class SaveMlNetModelDto: MlNetModel
    {
        public ITransformer AiModel { get; init; }
        public DataViewSchema Schema { get; init; }
    }
}
