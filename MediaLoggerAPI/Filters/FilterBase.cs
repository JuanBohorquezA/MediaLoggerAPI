using Amazon.Runtime.Internal;
using MediaLogger.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MediaLoggerAPI.Filters
{
    public abstract class FilterBase : Attribute, IAsyncActionFilter
    {
        protected void SetErrorResponse(ActionExecutingContext context, string errorDescription, string errorMessage, HttpStatusCode statusCode)
        {
            var errorResponse = new HttpErrorResponse()
            {
                description = errorDescription,
                message = errorMessage,
                statusCode = (int)statusCode,
            };
            context.Result = new ObjectResult(errorResponse) { StatusCode = (int)statusCode };
        }
        public abstract Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);
        
    }
}
