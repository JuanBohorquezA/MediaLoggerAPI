using MediaLogger.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.RegularExpressions;

namespace MediaLoggerAPI.Filters.BLValidator
{
    public class ValidateGetVideo : FilterBase
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? videoName = context.ActionArguments.ToList()[1].Value as string;
            string pattern = @"^\d+$";

            if (videoName == null)
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.EMPTYFIELDS, HttpStatusCode.BadRequest);
                return;
            }
            if(!Regex.IsMatch(videoName, pattern))
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.Error($"Id transaccion is not valid."), HttpStatusCode.BadRequest);
                return;
            }
            await next();

        }
    }
}
