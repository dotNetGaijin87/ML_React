using Microsoft.ML;
using ML.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Common.MlRepository
{
    /// <summary>
    /// Interface for <class cref="MlRepository"></class>
    /// </summary>
    public interface IMlRepository
    {
        bool SaveModel(SaveMlNetModelDto model);
        ITransformer LoadModel(MlNetModel model);
        bool DeleteModel(MlNetModel model);
        IEnumerable<string> ListModels(Entity modelFamily);

        Task<bool> SaveTrainingData(SaveTrainingData data);
        IDataView LoadTrainingData<T>(TrainingDataModel model) where T : class;
        IEnumerable<string> ListTrainingData(Entity modelFamily);
        bool DeleteTrainingData(TrainingDataModel model);
    }
}