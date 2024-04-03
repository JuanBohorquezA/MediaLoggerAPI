using MediaLogger.Domain.DTOs;
using MediaLogger.Domain;
using MediaLogger.Domain.Variables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace MediaLoggerAPI.Filters.AuthValidator
{
    public class ValidateApiKeyFilter : FilterBase
    {
        private readonly string _apiKeyRequired;
        public ValidateApiKeyFilter(string apiKeyRequired)
        {
            _apiKeyRequired = apiKeyRequired;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var apikey = typeof(AppSettings).GetProperty(_apiKeyRequired)?.GetValue(null)?.ToString();
            var apiKeyConfigValue = configuration.GetValue<string>(apikey);


            var apiKeyHeader = context.ActionArguments?.FirstOrDefault().Value?.ToString();

            if (string.IsNullOrEmpty(apiKeyHeader) || !apiKeyHeader.Equals(apiKeyConfigValue))
            {
                SetErrorResponse(context, ResponseMessage.UNAUTHORIZED, ResponseMessage.UNAUTHORIZED_MESSAGE("Invalid API key"), HttpStatusCode.Unauthorized);
                return;
            }
            await next();
        }

      
    }
}
