using MediaLogger.Application.Validation;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Security;

namespace MediaLogger.Application.BL
{
    public class AuthBL 
    {
        private readonly PayPadBL _payPadBL;
        private readonly PayPadValidation _payPadValidation;

        public AuthBL( PayPadBL payPadBL, PayPadValidation payPadValidation)
        {
            _payPadBL = payPadBL;
            _payPadValidation = payPadValidation;
        }


        public async Task<bool> Login(Login loginData)
        {

            PayPadDto? paypad = await _payPadBL.GetByUsernameAsync(loginData.UserName);
            if(paypad == null || paypad.Username == null) return false;

            paypad.Pwd = await _payPadBL.GetPaypadPasswordAsync(paypad.Username);
            var paypadResult = _payPadValidation.ValidatePassword(paypad, loginData.Password);
            if (paypadResult == null) return false;
            return true;
        }
       
    }
}
