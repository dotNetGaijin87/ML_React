using Microsoft.AspNetCore.Mvc;
using ML.Models;
using ML.Services;

namespace ML.Controllers
{
    /// <summary>
    /// Manages endpoints for anomaly detection based on 
    /// Super-resolution convolutional neural network (SRCNN) algorithm
    /// </summary>
    [ApiController]
    [Route("anomaly-detection")]
    public class SrCnnController : ControllerBase
    {
        private readonly ISrCnnService _srCnnService;

        public SrCnnController(ISrCnnService srCnnService)
        {
            _srCnnService = srCnnService;
        }

        /// <summary>
        /// Custom method for running anomaly detection of provided data.
        /// </summary>
        /// <param name="model">Contains training data and training parameters</param>
        /// <returns>Returns anomaly score and anomaly flag for each training data point provided</returns>
        [HttpPost("/anomaly-detection:Run")]
        public IActionResult Get(SrCnnOptions model)
        {
            var prediction = _srCnnService.Predict(model);

            return Ok(prediction);
        }
    }
}
