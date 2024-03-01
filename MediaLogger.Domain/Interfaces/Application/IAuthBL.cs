using MediaLogger.Domain.DTOs.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Application
{
    public interface IAuthBL
    {
        Task<bool> Login(Login loginData);
    }
}
