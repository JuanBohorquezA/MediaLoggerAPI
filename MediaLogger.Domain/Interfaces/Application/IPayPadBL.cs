using MediaLogger.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Application
{
    public interface IPayPadBL
    {
        Task<PayPadDto?> GetByUsernameAsync(string? username);
        Task<string?> GetPaypadPasswordAsync(string username);
    }
}
