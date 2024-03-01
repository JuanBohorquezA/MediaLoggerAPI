using MediaLogger.Domain.DTOs;
using MediaLogger.Domain;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using MediaLogger.Domain.Interfaces.Application.Validations;

namespace MediaLogger.Application.Validation
{
    public class PayPadValidation: IPayPadValidation
    {

        private readonly IConfiguration _configuration;


        public PayPadValidation( IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public PayPadDto? ValidatePassword(PayPadDto? paypadResult, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {

                if (paypadResult != null && !string.IsNullOrEmpty(paypadResult.Pwd) && password.ValidateEncodedPassword(paypadResult.Pwd, _configuration))
                {
                    return paypadResult;
                }
            }

            return null;
        }


    }
}
