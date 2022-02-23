using Microsoft.ML;
using ML.Core;
using ML.Helpers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Helpers
{
    public interface IMlRepository
    {
        bool SaveModel(SaveMlNetModelDto model);
        ITransformer LoadModel(MlNetModel model);

        bool DeleteModel(MlNetModel model);

        IEnumerable<string> ListModels(AiModelFamily modelFamily);

        Task<bool> SaveTrainingData(SaveTrainingData data);
        IDataView LoadTrainingData<T>(TrainingDataModel model) where T : class;
    }
}