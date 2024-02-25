using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
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
        private readonly MediaMiddleware _mediaLoggerMidleware;
        private readonly string? _videosDirectory;
        private readonly PayPadBL _payPadBL;
        private readonly LogBL _log;
        private readonly Token _token;
        public MediaLoggerController(IConfiguration configuration, MediaMiddleware mediaLoggerMidleware, PayPadBL payPadBL, LogBL log, Token token)
        {
            _videosDirectory = configuration.GetSection(AppSettings.VideosDirectory).Value; 
            _mediaLoggerMidleware = mediaLoggerMidleware;
            _payPadBL = payPadBL;
            _log = log;
           _token = token;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SaveVideo")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.InternalServerError)]
        private async Task<IActionResult?> SaveVideo([FromHeader(Name = "JWT")] string Jwt, IFormFile formFile)
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
        public async Task<IActionResult?> SaveLog([FromHeader(Name = "JWT")] string Jwt, [FromBody] SaveLogDto Log)
        {
            try
            {
                if (!_mediaLoggerMidleware.IsValidJwt(Jwt))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.Unauthorized, ResponseMessage.UNAUTHORIZED("Invalid JWT"), null);
                }

                var username = _token.GetNameFromToken(Jwt);
                PayPadDto? Paypad = await _payPadBL.GetByUsernameAsync(username);

                Log? log = await _log.InsertLog(Log, Paypad.Id ,Paypad?.Username);
               
                await EventLogger.AsyncSaveLog(ETypeLogApp.Info, $"SaveLog, fue Guardado.");
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Log insertion"), null);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"SaveLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }


        #region FileMethods

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
