using Dashboard.Domain.DTOs;
using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces.Application;
using MediaLogger.Domain.Interfaces.Application.Validations;
using MediaLogger.Domain.Variables;
using MediaLoggerAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaLoggerController : BaseController
    {
        private readonly IPayPadBL _payPadBL;
        private readonly ILogBL _log;
        private readonly ITokenBL _token;
        public MediaLoggerController(IPayPadBL payPadBL, ILogBL log, ITokenBL token)
        {
            _payPadBL = payPadBL;
            _log = log;
           _token = token;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("SaveLog")]
        [ValidateJwtFilter]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> SaveLog([FromHeader(Name = "JWT")] string Jwt, [FromBody] SaveLogDto Log)
        {
            try
            {

                var username = _token.GetNameFromToken(Jwt);
                PayPadDto? Paypad = await _payPadBL.GetByUsernameAsync(username);

                Log? log = await _log.InsertLog(Log, Paypad.Id ,Paypad?.Username);
               
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Log insertion"), null);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"SaveLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }





    }
}
