using Microsoft.AspNetCore.Mvc;
using ML.Common;
using ML.Services.Environment;
using System.Collections.Generic;
using System.Linq;

namespace ML.Controllers
{
    /// <summary>
    /// Provides information about available algorithms and ai models
    /// </summary>
    [ApiController]
    [Route("info")]
   
    public class InfoController : Controller
    {
        IInfoService _infoService;

        public InfoController(IInfoService service)
        {
            _infoService = service;
        }

        /// <summary>
        /// Returns a list of available ai models for a given category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryName}")]
        public Dictionary<string, IEnumerable<ModelTypeToUrl>> ListModels(string categoryName = null)
        {
            Dictionary<string, IEnumerable<ModelTypeToUrl>> modelDictionary = _infoService.ListModels(categoryName);

            return modelDictionary;
        }

        /// <summary>
        /// Returns a list of available ai model categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("category")]
        public IEnumerable<string> ListCategories()
        {
            List<string> modelDictionary = _infoService.ListCategories().ToList();

            return modelDictionary;
        }
    }
}
