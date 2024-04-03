using MediaLogger.Domain;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MediaLoggerAPI.Filters.BLValidator
{
    public class ValidateSaveLogReq : FilterBase
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            SaveLogDto? saveLogReq = context.ActionArguments.ToList()[1].Value as SaveLogDto;
            if(saveLogReq == null)
            {
                SetErrorResponse(context, ResponseMessage.INTERNALSERVERERROR, ResponseMessage.UNAUTHORIZED_MESSAGE("Casting SaveLogDto, on Filter"), HttpStatusCode.Unauthorized);
                return;
            }

            if (string.IsNullOrEmpty(saveLogReq?.Content) || string.IsNullOrEmpty(saveLogReq.Logtype))
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.EMPTYFIELDS, HttpStatusCode.BadRequest);
                return;
            }

            if(!Enum.GetNames(typeof(ETypeLogReq)).Any(x => x == saveLogReq.Logtype))
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.Error($"'{saveLogReq.Logtype}' doesn't exist in the enumerable"), HttpStatusCode.BadRequest);
                return;
            }
            await next();
        }
    }
}
