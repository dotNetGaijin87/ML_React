using Microsoft.ML;
using ML.Common.Cache;
using ML.Core;
using ML.Helpers.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Helpers
{
    /// <summary>
    /// Class used for loading and saving training data files and AI models
    /// </summary>
    public class MlRepository : IMlRepository
    {
        private readonly MLContext _mlContext;
        private readonly IDataPathRegister _dataPathRegister;


        public MlRepository(MLContext mlContext, IDataPathRegister dataPathRegister)
        {
            _mlContext = mlContext;
            _dataPathRegister = dataPathRegister;
        }

        /// <summary>
        /// Saves given model
        /// </summary>
        /// <param name="mlModel">Model to be saved</param>
        /// <param name="modelInputSchema">ML.NET MLContext object</param>
        /// <param name="type">Name of the type of the algorithm that is used in the model</param>
        /// <param name="modelName">Name of the trained model</param>
        /// <returns></returns>
        public bool SaveModel(SaveMlNetModelDto input)
        {
            string path = _dataPathRegister.GetModelFolderaPath(input.Category, input.Type);
            string modelFullName = $"{input.Name}.{input.Extension}";
            try
            {
                _mlContext.Model.Save(input.AiModel, input.Schema, Path.Combine(path, modelFullName));

                return true;
            }
            catch (Exception)
            {
                throw new SavingModelException(input.Name);
            }
        }

        /// <summary>
        /// Loads a model based on provided name and type
        /// </summary>
        /// <param name="type">Name of the type of the algorithm that is used in the model</param>
        /// <param name="modelName">Name of the trained model</param>
        /// <returns>ITransformer object that is used for creating ML.NET prediction engine</returns>
        public ITransformer LoadModel(MlNetModel model)
        {
            var preconditionCheck = _dataPathRegister.DoesModelExists(model.Category, model.Type, model.Name);
            if (!preconditionCheck)
                throw new ModelNotFoundException(model.Name);

            string path = _dataPathRegister.GetModelPath(model.Category, model.Type, model.Name);
            try
            {
                return _mlContext.Model.Load(path, out var modelInputSchema);
            }
            catch (Exception)
            {
                throw new ModelLoadingException(model.Name);
            }
        }

        public bool DeleteModel(MlNetModel model)
        {
            var preconditionCheck = _dataPathRegister.DoesModelExists(model.Category, model.Type, model.Name);
            if (!preconditionCheck)
                throw new ModelNotFoundException(model.Name);

            try
            {
                string modelPath = _dataPathRegister.GetModelPath(model.Category, model.Type, model.Name);
                File.Delete(modelPath);
                return true;
            }
            catch (Exception)
            {
                throw new DeletingModelException(model.Name);
            }
        }
        public IEnumerable<string> ListModels(AiModelFamily modelFamily)
        {
            var path = _dataPathRegister.GetModelFolderaPath(modelFamily.Category, modelFamily.Type);

            IEnumerable<string> modelNameList = Directory.GetFiles(path)
                                                    .Select(f => Path.GetFileName(f))
                                                    .Select(fn => fn.Substring(0, fn.LastIndexOf(".")))
                                                    .ToList();
            return modelNameList;
        }

        public async Task<bool> SaveTrainingData(SaveTrainingData data)
        {
            if (!data.OverrideIfAlreadyExists)
            {
                var preconditionCheck = _dataPathRegister.DoesTrainingDataExists(data.Category, data.Type, data.Name);
                if (preconditionCheck)
                    throw new TrainingDataAlreadyExistException(data.Name);
            }

            try
            {
                var filePath = _dataPathRegister.GetTrainingDataFolderPath(data.Category, data.Type);
                await File.WriteAllBytesAsync(Path.Combine(filePath, data.Name), data.Data);

                return true;
            }
            catch (Exception)
            {
                throw new SavingTrainingDataException(data.Name);
            }
        }

        public IDataView LoadTrainingData<T>(TrainingDataModel input) where T : class
        {
            string path = _dataPathRegister.GetTrainingDataFilePath(input.Category, input.Type, input.Name);

            try
            {
                return _mlContext.Data.LoadFromTextFile<T>(path: path, hasHeader: true, separatorChar: ',');
            }
            catch (Exception)
            {
                throw new TrainingDataLoadingException(input.Name);
            }
        }
    }
}
