using Microsoft.AspNetCore.Mvc;
using ML.Services.Regression;
using System.Collections.Generic;

namespace ML.Controllers
{
    /// <summary>
    /// Base class for regression based algorithms
    /// </summary>
    [ApiController]
    public abstract class RegressionControllerBase : ControllerBase
    {
        private  IRegressionServiceBase _service;

        public RegressionControllerBase(IRegressionServiceBase mainService)
        {
            _service = mainService;
        }


        /// <summary>
        /// Deletes an ai model resource
        /// </summary>
        /// <param name="modelName">Name of the model to be deleted</param>
        /// <returns>Result of the operation</returns>
        [HttpDelete]
        public virtual bool Delete(string modelName)
        {
            return _service.Delete(modelName);

        }

        /// <summary>
        /// Lists ai model resource names
        /// </summary>
        /// <returns>Model names</returns>
        [HttpGet]
        public virtual IEnumerable<string> List()
        {
            return _service.List();
        }


        /// <summary>
        /// A custom method for running an ai model on a single row of data
        /// </summary>
        /// <param name="request">Contains the name of the model and the data for running</param>
        /// <returns></returns>
        public virtual RegressionServiceRunSingleModelOutput RunSinglePrediction(RunSinglePredictionDto request)
        {

            RegressionServiceRunSingleModelOutput prediction = _service.RunSingle(request.ModelName, request.Data);
            return prediction;
        }

        /// <summary>
        /// A custom method for running an ai model on multiple data rows
        /// </summary>
        /// <param name="request">Contains the name of the model and the data for running</param>
        /// <returns></returns>
        public virtual RegressionServiceRunMultipleModelOutput RunMultiplePredictions(RunMultiplePredictionsRequest request)
        {
            RegressionServiceRunMultipleModelOutput prediction = _service.RunMultiple(request.ModelName, request.Data);
           
            return prediction;
        }
    }
}

