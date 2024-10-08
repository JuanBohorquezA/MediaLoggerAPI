﻿using MediaLogger.Domain.DTOs;
using MediaLogger.Aplication.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces.Application;
using MediaLoggerAPI.Filters;
using MediaLoggerAPI.Filters.BLValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediaLoggerAPI.Filters.AuthValidator;
using MediaLogger.Domain.Interfaces;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MediaLoggerController : BaseController
    {
        private readonly IPayPadBL _payPadBL;
        private readonly ILogManager _log;
        private readonly IVideoBL _videoBL;
        private readonly ITokenBL _token;
        public MediaLoggerController(IPayPadBL payPadBL, ILogManager log, IVideoBL videoBL, ITokenBL token)
        {
            _payPadBL = payPadBL;
            _log = log;
            _videoBL = videoBL;
           _token = token;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("SaveLog")]
        [ValidateJwtFilter]
        [ValidateSaveLogReq]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> SaveLog([FromHeader(Name = "JWT")] string Jwt, [FromBody] SaveLogDto Log)
        {
            try
            {
                var username = _token.GetNameFromToken(Jwt);
                PayPadDto? Paypad = await _payPadBL.GetPaypadByUsernameAsync(username);
                Log? log = await _log.LogService(Log, Paypad.Id ,Paypad?.Username);
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Log insertion"), log?.ID.ToString());
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"SaveLog, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }

        
        
        [AllowAnonymous]
        [HttpPost]
        [Route("SaveVideo")]
        [ValidateJwtFilter]
        [ValidateSaveVideoReq]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> SaveVideo([FromHeader(Name = "JWT")] string Jwt, IFormFile videoFile)
        {
            try
            {
                var username = _token.GetNameFromToken(Jwt);
                PayPadDto? Paypad = await _payPadBL.GetPaypadByUsernameAsync(username);
                await _videoBL.UploadVideo(videoFile);
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Video has been uploaded"),null);
            }
            catch (Exception ex)
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"SaveVideo, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }



    }
}
