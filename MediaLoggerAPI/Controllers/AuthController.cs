using MediaLogger.Aplication.BL;
using MediaLogger.Application.BL;
using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Security;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Persistence.SQLServer;
using MediaLoggerAPI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MediaLoggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly AuthMiddleware _middleware;
        private readonly AuthBL _authBL;
        private readonly Token _token;

        public AuthController(AuthMiddleware middleware, AuthBL authBL, Token token )
        {
            _middleware = middleware;
            _authBL = authBL;
            _token = token;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(HttpResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult?> Login([FromHeader (Name = "API-KEY-LOGIN")]string apiKey, [FromBody] Login login) 
        {
            try
            {   
                if (!_middleware.IsValidApiKeyLogin(apiKey))
                {
                    return await GetResponseAsync<object?>(HttpStatusCode.Unauthorized, ResponseMessage.UNAUTHORIZED("Invalid API key"), null);
                }

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
