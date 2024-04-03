using MediaLogger.Domain.DTOs;
using MediaLogger.Domain;
using MediaLogger.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MediaLoggerAPI.Filters.AuthValidator
{
    public class ValidateJwtFilter : FilterBase
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var JWT = context.ActionArguments?.FirstOrDefault().Value?.ToString();
            var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenBL)) as ITokenBL;
            if (string.IsNullOrEmpty(JWT) || !tokenService.IsJwtTokenValid(JWT))
            {
                SetErrorResponse(context, ResponseMessage.UNAUTHORIZED, ResponseMessage.UNAUTHORIZED_MESSAGE("Invalid JWT"), HttpStatusCode.Unauthorized);
                return;
            }
            await next();
        }
    }
}
