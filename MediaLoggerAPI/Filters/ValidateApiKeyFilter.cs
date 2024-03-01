using Dashboard.Domain.DTOs;
using MediaLogger.Domain;
using MediaLogger.Domain.Variables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace MediaLoggerAPI.Filters
{
    public class ValidateApiKeyFilter : Attribute, IAsyncActionFilter
    {
        private readonly string _apiKeyRequired;
        public ValidateApiKeyFilter(string apiKeyRequired)
        {
            _apiKeyRequired = apiKeyRequired;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var apikey = typeof(AppSettings).GetProperty(_apiKeyRequired).GetValue(null).ToString();
            var apiKeyConfigValue = configuration.GetValue<string>(apikey);
            
           
            var apiKeyHeader = context.ActionArguments.FirstOrDefault().Value.ToString();

            if (string.IsNullOrEmpty(apiKeyHeader) || !apiKeyHeader.Equals(apiKeyConfigValue))
            {
                var errorResponse = new HttpErrorResponse { description = ResponseMessage.UNAUTHORIZED, message = ResponseMessage.UNAUTHORIZED_MESSAGE("Invalid API key"), statusCode = (int)HttpStatusCode.Unauthorized };
                context.Result = new ObjectResult(errorResponse) { StatusCode = (int)HttpStatusCode.Unauthorized};
                return;
            }
            await next();
        }
    }
}
