using Microsoft.ML;
using ML.Core;

namespace ML.Common.MlRepository
{
    public class SaveMlNetModelDto: MlNetModel
    {
        public ITransformer AiModel { get; init; }
        public DataViewSchema Schema { get; init; }
    }
}
