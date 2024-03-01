using MediaLogger.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Application.Validations
{
    public interface IPayPadValidation
    {
        PayPadDto? ValidatePassword(PayPadDto? paypadResult, string password);

    }
}
