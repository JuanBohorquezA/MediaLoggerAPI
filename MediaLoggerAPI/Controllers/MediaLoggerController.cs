using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Variables;
using MediaLoggerAPI.Middleware;
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
        private readonly MediaLoggerMidleware _mediaLoggerMidleware;
        private readonly string _videosDirectory;
        private readonly string _logsDirectory;
        private readonly PayPadBL _payPadBL;
        private readonly Token _token;
        public MediaLoggerController(IConfiguration configuration, MediaLoggerMidleware mediaLoggerMidleware, PayPadBL payPadBL, Token token)
        {
            _videosDirectory = configuration.GetSection(AppSettings.VideosDirectory).Value ?? string.Empty;
            _logsDirectory = configuration.GetSection(AppSettings.LogsDirectory).Value ?? string.Empty; 
            _mediaLoggerMidleware = mediaLoggerMidleware;
            _payPadBL = payPadBL;
           _token = token;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SaveVideo")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> SaveVideo([FromHeader(Name = "JWT")] string Jwt, IFormFile formFile)
        {
            try
            {
                if (!_mediaLoggerMidleware.IsValidJwt(Jwt))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.Unauthorized, ResponseMessage.UNAUTHORIZED("Invalid JWT"), null);
                }

                if (formFile == null || formFile.Length == 0)
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ResponseMessage.EMPTYFIELDS, null);
                }

                var filename = formFile.FileName;
                var username = _token.GetNameFromToken(Jwt);

                PayPadDto? Paypad = await _payPadBL.GetByUsernameAsync(username);
                await WriteVideo(Paypad, formFile);

                await EventLogger.AsyncSaveLog(ETypeLogApp.Info, $"SaveVideo, El video '{formFile.FileName}' fue Guardado.");
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Video sent"), null);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"SaveVideo, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.InternalServerError, ResponseMessage.INTERNALSERVERERROR(ex.Message), null);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SaveLog")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> SaveLog([FromHeader(Name = "JWT")] string Jwt, [FromBody] ReqLog saveFile)
        {
            try
            {
                if (!_mediaLoggerMidleware.IsValidJwt(Jwt))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.Unauthorized, ResponseMessage.UNAUTHORIZED("Invalid JWT"), null);
                }

                if ( string.IsNullOrEmpty(saveFile.Content))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ResponseMessage.EMPTYFIELDS, null);
                }
                if(!Enum.IsDefined(typeof(ETypeLogReq), saveFile.Logtype))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ResponseMessage.Error($"'{saveFile.Logtype}' doesn't exist in the enumerable"), null);
                }
                var username = _token.GetNameFromToken(Jwt);
                PayPadDto? Paypad = await _payPadBL.GetByUsernameAsync(username);

                WriteFile(saveFile, Paypad);
                await EventLogger.AsyncSaveLog(ETypeLogApp.Info, $"SaveLog, El Log '{saveFile}' fue Guardado.");
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Log created"), null);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"SaveLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.InternalServerError, ResponseMessage.INTERNALSERVERERROR(ex.Message), null);
            }
        }


        #region FileMethods


        private async void WriteFile(ReqLog reqLog, PayPadDto padDto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(reqLog.Content, Formatting.Indented);

                var logDir = Path.Combine(_logsDirectory, padDto.Username, padDto.Office, reqLog.Logtype.ToString());
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
                var fileName = "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
                var filePath = Path.Combine(logDir, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    var archivo = System.IO.File.CreateText(filePath);
                    archivo.Close();
                }

                using (StreamWriter sw = System.IO.File.AppendText(filePath))
                {
                    sw.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"WriteFile, Error: {ex.Message}");
            }

        }
        private async Task WriteVideo(PayPadDto padDto, IFormFile formFile)
        {
            var videoPath = Path.Combine(_videosDirectory, padDto.Username, padDto.Office);
            if (!Directory.Exists(videoPath))
            {
                Directory.CreateDirectory(videoPath);
            }
            var filePath = Path.Combine(videoPath, formFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }


        #endregion


    }
}
