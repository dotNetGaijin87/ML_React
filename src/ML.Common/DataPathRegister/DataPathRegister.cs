using ML.Core;
using System.IO;
using System.Linq;

namespace ML.Common.DataPathRegister
{
    /// <summary>
    /// Contains file paths for training data and model files.
    /// Each model and training data files are divided based on algorithms they are used for.
    /// </summary>
    public class DataPathRegister : IDataPathRegister
    {
        private readonly string TrainingDataBasePath;
        private readonly string MlModelsBasePath;

        public DataPathRegister(string trainingDataBasePath, string mlModelBasePath)
        {
            TrainingDataBasePath = trainingDataBasePath;
            MlModelsBasePath = mlModelBasePath;
        }

        /// <summary>
        /// Gets a ml model path based on its category, type and a name
        /// </summary>
        /// <param name="modelCategory"></param>
        /// <param name="modelType"></param>
        /// <param name="modelName">Ml model name</param>
        /// <returns></returns>
        public string GetModelPath(MlCategory modelCategory, MlAlgorithm modelType, string modelName)
        {
            string basePath = GetModelFolderaPath(modelCategory, modelType);
            var filePath = Directory.GetFiles(basePath)
                                    .Where(filPath => Path.GetFileNameWithoutExtension(modelName) == Path.GetFileNameWithoutExtension(filPath))
                                    .ToList();

            if (filePath.Count() == 0)
            {
                throw new ModelNotFoundException(modelName);
            }
            else if (filePath.Count() > 1)
            {
                throw new MultipleModelsWithSameNameFoundException(modelName);
            }

            return filePath.First();
        }

        /// <summary>
        /// Gets a training data file path based on the ml model it is used for training
        /// </summary>
        /// <param name="modelCategory"></param>
        /// <param name="modelType"></param>
        /// <param name="fileName">Training data name</param>
        /// <returns></returns>
        public string GetTrainingDataFilePath(MlCategory modelCategory, MlAlgorithm modelType, string fileName)
        {
            string basePath = GetTrainingDataFolderPath(modelCategory, modelType);
            var filePath = Directory.GetFiles(basePath)
                                    .Where(filPath => Path.GetFileNameWithoutExtension(fileName) == Path.GetFileNameWithoutExtension(filPath))
                                    .ToList();

            if (filePath.Count() == 0)
            {
                throw new TrainingDataNotFoundException(fileName);
            }
            else if (filePath.Count() > 1)
            {
                throw new MultipleTrainingDataFilesWithSameNameFoundException(fileName);
            }

            return filePath.First();
        }
 

        /// <summary>
        /// Returns a base path for training data for models with common category and type
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="modelTypeName"></param>
        /// <returns></returns>
        public string GetTrainingDataFolderPath(MlCategory categoryName, MlAlgorithm modelTypeName)
        {
            return Path.Combine(TrainingDataBasePath, categoryName.ToString(), modelTypeName.ToString());
        }

        /// <summary>
        /// Returns a base path for models with common category and type
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="modelTypeName"></param>
        /// <returns></returns>
        public string GetModelFolderaPath(MlCategory categoryName, MlAlgorithm modelTypeName)
        {
            return Path.Combine(MlModelsBasePath, categoryName.ToString(), modelTypeName.ToString());
        }

        /// <summary>
        /// Checks if training data exists
        /// </summary>
        /// <param name="category"></param>
        /// <param name="modelType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DoesTrainingDataExists(MlCategory category, MlAlgorithm modelType, string name)
        {
            string basePath = GetTrainingDataFolderPath(category, modelType);
            return Directory.GetFiles(basePath)
                                .Where(filPath => Path.GetFileNameWithoutExtension(name) == Path.GetFileNameWithoutExtension(filPath))
                                .Any();
        }

        /// <summary>
        /// Checks if ml model exists
        /// </summary>
        /// <param name="category"></param>
        /// <param name="modelType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DoesModelExists(MlCategory category, MlAlgorithm modelType, string name)
        {
            string basePath = GetModelFolderaPath(category, modelType);
            return Directory.GetFiles(basePath)
                                .Where(filPath => Path.GetFileNameWithoutExtension(name) == Path.GetFileNameWithoutExtension(filPath))
                                .Any();
        }

    }
}
