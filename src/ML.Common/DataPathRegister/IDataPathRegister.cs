using ML.Core;

namespace ML.Common.DataPathRegister
{
    /// <summary>
    /// Interface for <class cref="DataPathRegister"></class>
    /// </summary>
    public interface IDataPathRegister
    {
        bool DoesModelExists(MlCategory category, MlAlgorithm modelType, string name);
        string GetModelFolderaPath(MlCategory category, MlAlgorithm modelType);
        string GetModelPath(MlCategory category, MlAlgorithm modelType, string modelName);

        bool DoesTrainingDataExists(MlCategory category, MlAlgorithm modelType, string name);
        string GetTrainingDataFilePath(MlCategory category, MlAlgorithm modelType, string fileName);
        string GetTrainingDataFolderPath(MlCategory category, MlAlgorithm modelType);
    }
}