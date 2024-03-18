using Dashboard.Domain.DTOs;
using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Security;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces.Application;
using MediaLogger.Domain.Interfaces.Application.Validations;
using MediaLogger.Domain.Variables;
using MediaLogger.Persistence.SQLServer;
using MediaLoggerAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthBL _authBL;
        private readonly ITokenBL _token;

        public AuthController(IAuthBL authBL, ITokenBL token )
        {
            _authBL = authBL;
            _token = token;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [ValidateApiKeyFilter("Login_Api_Key")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> Login([FromHeader (Name = "API-KEY-LOGIN")]string apiKey, [FromBody] Login login) 
        {
            try
            {   
                try
                {
                    login.Password = Encryption.DecryptRSA(login.Password);
                }
                catch (Exception ex)
                {
                    await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"Login, Error al desencriptar la contraseña: {ex.Message}");
                    return await GetResponseAsync<string?>(HttpStatusCode.BadRequest, "Usuario y/o contraseña incorrecto", null);
                }

                await _authBL.Login(login);
                var token = _token.GenerateJwtToken(login);

                string? clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                await EventLogger.AsyncSaveLog(ETypeLogApp.Info, $"Login, Inicio de sección. Username: {login.UserName}", clientIpAddress ?? "");
                return await GetResponseAsync<object?>(HttpStatusCode.OK, ResponseMessage.OK("Login"), token);
            }
            catch (Exception ex) 
            {
                await EventLogger.AsyncSaveLog(ETypeLogApp.Error, $"Login, Error: {ex.Message}");
                return await GetResponseAsync<object?>(HttpStatusCode.BadRequest, ex.Message, null);
            }
        }

    }
}
