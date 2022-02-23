using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ML.Controllers.TrainingData;
using System.Net;

namespace ML.Filters
{
    /// <summary>
    /// Checks if a submitted form is not empty
    /// </summary>
    public class EnsureFormIsNotEmptyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = (UploadTrainingDataRequest)context.ActionArguments["request"];
           if(!(request.FormFile.Length > 2))
            {
                var error = new ProblemDetails
                {
                    Title = "An error occurred",
                    Detail = "File is empty",
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "https://httpstatuses.com/400"

                };

                context.Result = new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }
    }
}
