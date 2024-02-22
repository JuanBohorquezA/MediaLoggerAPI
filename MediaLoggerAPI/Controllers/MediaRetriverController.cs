using MediaLogger.Aplication.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLoggerAPI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaRetriverController : BaseController
    {
        private readonly MediaMiddleware _mediaLoggerMidleware;
        private readonly LogBL _log;
        public MediaRetriverController(MediaMiddleware mediaMiddleware, LogBL log)
        {
            _mediaLoggerMidleware = mediaMiddleware;
            _log = log;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("GetLog")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> GetLog([FromHeader(Name = "JWT")] string Jwt, GetLogDto getLog)
        {
            try
            {
                if (!_mediaLoggerMidleware.IsValidJwt(Jwt))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.Unauthorized, ResponseMessage.UNAUTHORIZED("Invalid JWT"), null);
                }
                IEnumerable<Log>? logs = await _log.GetLogs(getLog);
                await EventLogger.AsyncSaveLog(ETypeLogApp.Info, $"GetLog, Consulta fue existosa.");
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("The consult"), logs);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"GetLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }
    }
}
