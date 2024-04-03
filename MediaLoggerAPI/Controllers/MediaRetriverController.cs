using MediaLogger.Domain.DTOs;
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
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using MediaLoggerAPI.Filters.AuthValidator;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MediaRetriverController : BaseController
    {

        private readonly ILogBL _log;
        private readonly IPayPadBL _payPadBL;
        public MediaRetriverController( ILogBL log, IPayPadBL payPadBL)
        {
            _log = log;
            _payPadBL = payPadBL;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("DownloadLog")]
        [ValidateApiKeyFilter("GetLog_Api_Key")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> DownloadLog([FromHeader(Name = "X-DOWNLOAD-API-KEY")] string ApiKey, GetLogDto getLog)
        {
            try
            {

                var logs = await _log.GetLogs(getLog);
                var logContent = ConcatenateLogs(logs);
                var fileName = await GetName(getLog);
                return FileContentResult(logContent, fileName);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"GetLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }
        #region Download Logs methods
        private async Task<string> GetName(GetLogDto getLog)
        {
            var payPad = await _payPadBL.GetPaypadByIdAsync(getLog.IdPaypad);
            var date = $"del-{getLog.StartDate:yyyy/MM/dd}-al-{getLog.FinalDate:yyyy/MM/dd}";
            return $"log-{payPad?.Username?.Replace("+", "")}-{date}";
        }
        private IActionResult FileContentResult(string content, string fileName)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return File(stream, "text/plain", fileName);
        }
        private string ConcatenateLogs(IEnumerable<string?>? logs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[\n");

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

            sb.Append("\n]");
            return sb.ToString();
        }
        #endregion

    }
}
