using Dashboard.Domain.DTOs;
using MediaLogger.Domain;
using MediaLogger.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MediaLoggerAPI.Filters
{
    public class ValidateJwtFilter:Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var JWT = context.ActionArguments.FirstOrDefault().Value.ToString();
            var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenBL)) as ITokenBL;
            if(string.IsNullOrEmpty(JWT) || !tokenService.IsJwtTokenValid(JWT)) 
            {
                var errorResponse = new HttpErrorResponse { description = ResponseMessage.UNAUTHORIZED, message = ResponseMessage.UNAUTHORIZED_MESSAGE("Invalid JWT"), statusCode = (int)HttpStatusCode.Unauthorized };
                context.Result = new ObjectResult(errorResponse) { StatusCode = (int)HttpStatusCode.Unauthorized };
                return;
            }
            await next();
        }
    }
}
