using Microsoft.AspNetCore.Mvc;
using ML.AiModels.Forecasting;
using ML.Services.Forecasting;

namespace ML.Controllers
{
    /// <summary>
    /// Manages endpoints for forecasting based on 
    /// Singular spectrum analysis (Ssa) algorithm
    /// </summary>
    [ApiController]
    [Route("forecasting")]
    public class SsaForecastingController : ControllerBase
    {
        ISsaForecastingService _ssaForecastingService;

        public SsaForecastingController(ISsaForecastingService ssaForecastingService)
        {
            _ssaForecastingService = ssaForecastingService;
        }

        /// <summary>
        /// Creates a model resource
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ssa")]
        public SsaForecastingOutput Create(CreateSsaForecastingDto request)
        {
            var forecast = _ssaForecastingService.Create(request);
  
            return forecast;
        }

    }
}
