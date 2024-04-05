using MediaLogger.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.RegularExpressions;

namespace MediaLoggerAPI.Filters.BLValidator
{
    public class ValidateSaveVideoReq : FilterBase
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IFormFile? file = context.ActionArguments.ToList()[1].Value as IFormFile;
            string pattern = @"^\d{1,}_\d{1,}_\d{8}_source\d$";

            if (file == null || file.Length == 0)
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.EMPTYFIELDS, HttpStatusCode.BadRequest);
                return;
            }
            if (!file.FileName.Contains(".mp4"))
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.Error($"data formating is not correct"), HttpStatusCode.BadRequest);
                return;
            }
            var videoName = file.FileName.Replace(".mp4", "");
            if (!Regex.IsMatch(videoName, pattern))
            {
                SetErrorResponse(context, ResponseMessage.BADREQUEST, ResponseMessage.Error($"Video name have not correct format"), HttpStatusCode.BadRequest);
                return;
            }

            await next();
        }
    }
}
