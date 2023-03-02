using App.Api.Contracts.Common;
using App.Domain.Entities.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Api.Filters
{
    /// <summary>
    /// Information of AppExceptionAttibute
    /// CreatedBy: ThiepTT(27/02/2023)
    /// </summary>
    public class AppExceptionAttibute : ExceptionFilterAttribute
    {
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context">ExceptionContext</param>
        /// CreatedBy: ThiepTT(27/02/2023)
        public override void OnException(ExceptionContext context)
        {
            var apiError = new OperationResult<bool>();
            apiError.Data = false;
            apiError.AddError(ErrorCode.ServerError, SystemConfig.ERRORSERVER);

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}