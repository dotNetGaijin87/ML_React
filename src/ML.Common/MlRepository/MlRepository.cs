using Microsoft.ML;
using ML.Common.DataPathRegister;
using ML.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Common.MlRepository
{
    /// <summary>
    /// Used for loading and saving training data files and Ml models
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
        /// Saves a given ml model
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
        /// Loads a ml model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes a ml model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
     
        /// <summary>
        /// Lists model names
        /// </summary>
        /// <param name="modelFamily"></param>
        /// <returns></returns>
        public IEnumerable<string> ListModels(Entity modelFamily)
        {
            var path = _dataPathRegister.GetModelFolderaPath(modelFamily.Category, modelFamily.AlgorithmType);

            IEnumerable<string> modelNameList = Directory.GetFiles(path)
                                                    .Select(f => Path.GetFileName(f))
                                                    .Select(fn => fn.Substring(0, fn.LastIndexOf(".")))
                                                    .ToList();
            return modelNameList;
        }

        /// <summary>
        /// Saves training data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Loads training data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public IDataView LoadTrainingData<T>(TrainingDataModel input) where T : class
        {
            string path = _dataPathRegister.GetTrainingDataFilePath(input.Category, input.AlgorithmType, input.Name);

            try
            {
                return _mlContext.Data.LoadFromTextFile<T>(path: path, hasHeader: true, separatorChar: ',');
            }
            catch (Exception)
            {
                throw new TrainingDataLoadingException(input.Name);
            }
        }

        /// <summary>
        /// Deletes training data
        /// </summary>
        /// <param name="trainingData"></param>
        /// <returns></returns>
        public bool DeleteTrainingData(TrainingDataModel trainingData)
        {
            var preconditionCheck = _dataPathRegister.DoesTrainingDataExists(trainingData.Category, trainingData.AlgorithmType, trainingData.Name);
            if (!preconditionCheck)
                throw new TrainingDataNotFoundException(trainingData.Name);

            try
            {
                string modelPath = _dataPathRegister.GetTrainingDataFilePath(trainingData.Category, trainingData.AlgorithmType, trainingData.Name);
                File.Delete(modelPath);
                return true;
            }
            catch (Exception)
            {
                throw new DeletingTrainingDataException(trainingData.Name);
            }
        }

        /// <summary>
        /// Lists training data names
        /// </summary>
        /// <param name="modelFamily"></param>
        /// <returns></returns>
        public IEnumerable<string> ListTrainingData(Entity modelFamily)
        {
            var path = _dataPathRegister.GetTrainingDataFolderPath(modelFamily.Category, modelFamily.AlgorithmType);

            IEnumerable<string> dataList = Directory.GetFiles(path)
                                                    .Select(f => Path.GetFileName(f))
                                                    .Select(fn => fn.Substring(0, fn.LastIndexOf(".")))
                                                    .ToList();
            return dataList;
        }
    }
}
