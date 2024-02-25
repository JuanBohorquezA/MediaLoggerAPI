using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLoggerAPI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MediaRetriverController : BaseController
    {
        private readonly MediaMiddleware _mediaLoggerMidleware;
        private readonly LogBL _log;
        private readonly Token _token;
        public MediaRetriverController(MediaMiddleware mediaMiddleware, LogBL log, Token token)
        {
            _mediaLoggerMidleware = mediaMiddleware;
            _log = log;
            _token = token;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("DownloadLog")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> DownloadLog([FromHeader(Name = "JWT")] string Jwt, GetLogDto getLog)
        {
            try
            {
                if (!_mediaLoggerMidleware.IsValidJwt(Jwt))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.Unauthorized, ResponseMessage.UNAUTHORIZED("Invalid JWT"), null);
                }

                var logs = await _log.GetLogs(getLog);
                var logContent = ConcatenateLogs(logs);
                var fileName = GetName(Jwt, getLog);
                await EventLogger.AsyncSaveLog(ETypeLogApp.Info, $"GetLog, Consulta fue existosa.");
                return FileContentResult(logContent, fileName);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"GetLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }
        #region Download Logs methods
        private string GetName(string JWT, GetLogDto getLog)
        {
            var username = _token.GetNameFromToken(JWT);
            var date = $"del-{getLog.StartDate:yyyy/mm/dd}-al-{getLog.FinalDate:yyyy/mm/dd}";
            return $"log-{username}-{date}";
        }
        private IActionResult FileContentResult(string content, string fileName)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return File(stream, "text/plain", fileName);
        }
        private string ConcatenateLogs(IEnumerable<string?>? logs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            bool first = true;
            foreach (var log in logs)
            {
                if (!first)
                {
                    sb.Append(", ");
                }
                sb.Append($"\"{log}\"");
                first = false;
            }

            sb.Append("]");
            return sb.ToString();
        }
        #endregion

    }
}
