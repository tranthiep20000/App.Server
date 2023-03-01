using App.Api.Contracts.Common;
using App.Domain.Entities.Results;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    /// <summary>
    /// Information of BaseController
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// HandlerErrorResponse
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>IActionResult</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        protected IActionResult HandlerErrorResponse(Error error)
        {
            var apiError = new ErrorResponse();

            if (error.Code == ErrorCode.NotFound)
            {
                apiError.StatusCode = 404;
                apiError.StatusPhrase = SystemConfig.NotFound;
                apiError.TimeStamp = DateTime.Now;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }

            apiError.StatusCode = 500;
            apiError.StatusPhrase = SystemConfig.InternalServerError;
            apiError.TimeStamp = DateTime.Now;
            apiError.Errors.Add(error.Message);

            return StatusCode(500, apiError);
        }
    }
}