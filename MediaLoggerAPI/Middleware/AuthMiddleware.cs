using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MediaLogger.Domain.Variables;

namespace MediaLoggerAPI.Middleware
{
    public class AuthMiddleware
    {
        private readonly string? _apiKey;
       
        public AuthMiddleware(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection(AppSettings.Login_Api_Key).Value;
        }

        public bool IsValidApiKeyLogin(string apiKey)
        {
            try
            {
                if (string.IsNullOrEmpty(apiKey)) return false;
                return apiKey.Equals(_apiKey);
            }
            catch
            {
                return false;
            }
        }

    }
}
