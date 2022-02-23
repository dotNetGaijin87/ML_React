using Microsoft.AspNetCore.Mvc;
using ML.Models;
using ML.Services;
using ML.Services.Regression;
using System.Text.Json;

namespace ML.Controllers
{
    /// <summary>
    /// Manages endpoints for data analysis based on the FastForest regression alogrithm
    /// </summary>
    [Route("regression/sdca")]
    public class SdcaController : RegressionControllerBase
    {
        ISdcaRegressionService _service;
        public SdcaController(ISdcaRegressionService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates an ai model resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CreateSdcaRegressionModelOptions request)
        {
            RegressionServiceCreateModelOutput metrics = _service.Create(request);

            return Ok(JsonSerializer.Serialize(metrics));
        }

        /// <summary>
        /// A custom method for running an ai model on a single row of data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/regression/sdca:RunSinglePrediction")]
        public override RegressionServiceRunSingleModelOutput RunSinglePrediction(RunSinglePredictionDto request)
        {
            return base.RunSinglePrediction(request);
        }

        /// <summary>
        /// A custom method for running an ai model on multiple data rows
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/regression/sdca:RunMultiplePredictions")]
        public override RegressionServiceRunMultipleModelOutput RunMultiplePredictions(RunMultiplePredictionsRequest request)
        {
            return base.RunMultiplePredictions(request);
        }
    }
}

