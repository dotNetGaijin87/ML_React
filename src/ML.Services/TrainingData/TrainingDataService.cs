using ML.Common.DataPathRegister;
using ML.Common.MlRepository;
using ML.Core;
using ML.Core.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ML.Services
{
    /// <summary>
    /// Service layer for managing training data used by ml models for training
    /// </summary>
    public class TrainingDataService : ITrainingDataService
    {
        private readonly IDataPathRegister _dataPathRegister;
        private readonly IMlRepository _mlRepository;

        
        public TrainingDataService(IDataPathRegister dataPathRegister, IMlRepository mlRepository)
        {
            _dataPathRegister = dataPathRegister;
            _mlRepository = mlRepository;
        }

        /// <summary>
        /// Creates new training data 
        /// </summary>
        /// <param name="trainingData"></param>
        /// <returns></returns>
        public async Task<bool> Create(CreateCsvTrainingDataDto trainingData)
        {
            MlCategory category = Converters.GetCategoryFromString(trainingData.ModelCategoryName);
            MlAlgorithm modelType = Converters.GetAiAlgorithmFromString(trainingData.ModelTypeName);

            var saveData = new SaveTrainingData
            {
                Category = category,
                Type = modelType,
                Name = trainingData.FileName,
                Data = trainingData.File
            };

            return await _mlRepository.SaveTrainingData(saveData);
        }

        /// <summary>
        /// Lists training data names
        /// </summary>
        /// <param name="trainingData"></param>
        /// <returns></returns>
        public IEnumerable<string> List(CsvTrainingDataFile trainingData)
        {
            MlCategory category = Converters.GetCategoryFromString(trainingData.ModelCategoryName);
            MlAlgorithm modelType = Converters.GetAiAlgorithmFromString(trainingData.ModelTypeName);

            var model = new Entity
            {
                Category = category,
                 AlgorithmType = modelType
            };


            return _mlRepository.ListTrainingData(model);
        }

        /// <summary>
        /// Deletes training data 
        /// </summary>
        /// <param name="trainingData"></param>
        /// <returns></returns>
        public bool Delete(CsvTrainingDataFile trainingData)
        {
            MlCategory category = Converters.GetCategoryFromString(trainingData.ModelCategoryName);
            MlAlgorithm modelType = Converters.GetAiAlgorithmFromString(trainingData.ModelTypeName);

            var data = new TrainingDataModel
            {
                Category = category,
                AlgorithmType = modelType,
                 Name = trainingData.FileName
            };

            
            return _mlRepository.DeleteTrainingData(data);
        }

        /// <summary>
        /// Extracts column headers from training data
        /// </summary>
        /// <param name="trainingData"></param>
        /// <returns></returns>
        public string[] GetHeaders(CsvTrainingDataFile trainingData)
        {
            MlCategory category = Converters.GetCategoryFromString(trainingData.ModelCategoryName);
            MlAlgorithm modelType = Converters.GetAiAlgorithmFromString(trainingData.ModelTypeName);

            var filePath = _dataPathRegister.GetTrainingDataFilePath(category, modelType, trainingData.FileName);
            string[] hedaers;
            using (var reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();
                hedaers =  line.Split(',');
            }

            return hedaers;
        }
    }
}
