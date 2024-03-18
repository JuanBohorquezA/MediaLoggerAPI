using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Security;
using MediaLogger.Domain.Interfaces.Application;
using MediaLogger.Domain.Interfaces.Application.Validations;

namespace MediaLogger.Application.BL
{
    public class AuthBL : IAuthBL
    {
        private readonly IPayPadBL _payPadBL;
        private readonly IPayPadValidation _payPadValidation;

        public AuthBL(IPayPadBL payPadBL, IPayPadValidation payPadValidation)
        {
            _payPadBL = payPadBL;
            _payPadValidation = payPadValidation;
        }


        public async Task<bool> Login(Login loginData)
        {
            if (string.IsNullOrEmpty(loginData.UserName) || string.IsNullOrEmpty(loginData.Password)) throw new Exception(ResponseMessage.EMPTYFIELDS);
            PayPadDto? paypad = await _payPadBL.GetPaypadByUsernameAsync(loginData.UserName);
            if(paypad == null || paypad.Username == null) return false;

            paypad.Pwd = await _payPadBL.GetPaypadPasswordAsync(paypad.Username);
            var paypadResult = _payPadValidation.ValidatePassword(paypad, loginData.Password);
            if (paypadResult == null) throw new Exception(ResponseMessage.Error("Username or password are incorrect"));
            return true;
        }
       
    }
}
