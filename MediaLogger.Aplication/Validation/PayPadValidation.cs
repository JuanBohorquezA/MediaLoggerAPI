using MediaLogger.Domain.DTOs;
using MediaLogger.Domain;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace MediaLogger.Application.Validation
{
    public class PayPadValidation
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

        public void ValidatePaypad(ref PayPadDto paypad)
        {
            //Validacion de campos obligatorios
            if (string.IsNullOrEmpty(paypad.Username)) throw new Exception("No se proporcionó nombre de Paypad");
            if (string.IsNullOrEmpty(paypad.Longitude) || string.IsNullOrEmpty(paypad.Latitude))
                throw new Exception("No se proporcionaron datos de ubicación de Paypad");
            if (!Convert.ToBoolean(paypad.IdCurrency)) throw new Exception("No se proporcionó id de Moneda");
            if (!Convert.ToBoolean(paypad.IdOffice)) throw new Exception("No se proporcionó id de Oficina");

            paypad.Username = paypad.Username.Trim();
            paypad.Longitude = paypad.Longitude.Trim();
            paypad.Latitude = paypad.Latitude.Trim();
            if (!string.IsNullOrEmpty(paypad.Description)) paypad.Description = paypad.Description.Trim();

            //Validar que la ubicación se puede transformar a decimal
            try
            {
                var longitudeFloat = Convert.ToDouble(paypad.Longitude);
                var latitudeFloat = Convert.ToDouble(paypad.Latitude);
            }
            catch (FormatException)
            {
                throw new Exception("Los datos de ubicación no son válidos como decimales o numeros");
            }

        }

        public void ValidatePaypadUpdate(ref PayPadDto paypad)
        {
            ValidatePaypad(ref paypad);
            if (paypad.IdUserUpdated <= 0) throw new Exception("No se proporcionó id de usuario actualización");
        }


    }
}
