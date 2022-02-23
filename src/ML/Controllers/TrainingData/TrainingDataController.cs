using Microsoft.AspNetCore.Mvc;
using ML.Controllers.TrainingData;
using ML.Filters;
using ML.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ML.Controllers
{
    /// <summary>
    /// Manages endpoints for storing, retrieving, deleting training data files used for training AI algorithms.
    /// </summary>
    [ApiController]
    [Route("training-data")]
    public class TrainingDataController : ControllerBase
    {
        readonly ITrainingDataService _trainingDataService;
        public TrainingDataController(ITrainingDataService trainingDataService)
        {
            _trainingDataService = trainingDataService;
        }


        /// <summary>
        /// Saves a training data file
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, EnsureFormIsNotEmpty]
        public async Task<bool> Create([FromForm] UploadTrainingDataRequest request)
        {
            byte[] trainingFile = null;
            using (var ms = new MemoryStream())
            {
                await request.FormFile.CopyToAsync(ms);
                trainingFile = ms.ToArray();
            }

            var serviceModel = new CreateCsvTrainingDataDto
            {
                ModelCategoryName = request.ModelCategoryName,
                ModelTypeName = request.ModelTypeName,
                FileName = request.FormFile.FileName,
                File = trainingFile
            };

            bool createResult =  await _trainingDataService.Create(serviceModel);

            return createResult;

        }

        /// <summary>
        /// Deletes a training data file
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(SingleTrainingDataRequest request)
        {
            var serviceModel = new CsvTrainingDataFile
            {
                ModelCategoryName = request.ModelCategoryName,
                ModelTypeName = request.ModelTypeName,
                FileName = request.FileName
            };

            var result = _trainingDataService.Delete(serviceModel);

            return result;
        }

        /// <summary>
        /// Lists training data files
        /// </summary>
        /// <param name="modelCategoryName"></param>
        /// <param name="modelTypeName"></param>
        /// <returns></returns>
        [HttpGet("{modelCategoryName}/{modelTypeName}")]
        public IEnumerable<string> List(string modelCategoryName, string modelTypeName)
        {
            var serviceModel = new CsvTrainingDataFile
            {
                ModelCategoryName = modelCategoryName,
                ModelTypeName = modelTypeName,
            };

            IEnumerable<string> trainingDataFileList = _trainingDataService.List(serviceModel);

            return trainingDataFileList;
        }

        /// <summary>
        /// Custom method for retrieving header names of a training data file
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/training-data:GetHeaders")]
        public string[] GetHeaders(SingleTrainingDataRequest request)
        {
            var serviceModel = new CsvTrainingDataFile
            {
                ModelCategoryName = request.ModelCategoryName,
                ModelTypeName = request.ModelTypeName,
                FileName = request.FileName
            };

            string[] headers = _trainingDataService.GetHeaders(serviceModel);

            return headers;
        }
    }
}
