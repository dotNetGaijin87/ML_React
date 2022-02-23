using Microsoft.AspNetCore.Mvc;
using ML.Models;
using ML.Services;
using ML.Services.Regression;
using System.Text.Json;

namespace ML.Controllers
{
    /// <summary>
    /// Manages endpoints for data analysis based on the FastTree regression alogrithm
    /// </summary>
    [Route("regression/fast-tree")]
    public class FastTreeController : RegressionControllerBase
    {
        IFastTreeRegressionService _service;

        public FastTreeController(IFastTreeRegressionService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates an ai model resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CreateFastTreeRegressionModelOptions request)
        {

            RegressionServiceCreateModelOutput metrics = _service.Create(request);

            return Ok(JsonSerializer.Serialize(metrics));
        }

        /// <summary>
        /// A custom method for running an ai model on a single row of data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/regression/fast-tree:RunSinglePrediction")]
        public override RegressionServiceRunSingleModelOutput RunSinglePrediction(RunSinglePredictionDto request)
        {
            return base.RunSinglePrediction(request);
        }

        /// <summary>
        /// A custom method for running an ai model on multiple data rows
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/regression/fast-tree:RunMultiplePredictions")]
        public override RegressionServiceRunMultipleModelOutput RunMultiplePredictions(RunMultiplePredictionsRequest request)
        {
            return base.RunMultiplePredictions(request);
        }
    }
}

