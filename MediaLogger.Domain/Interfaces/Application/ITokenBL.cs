using MediaLogger.Domain.DTOs.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Application
{
    public interface ITokenBL
    {
        string? GenerateJwtToken(Login login);
        bool IsJwtTokenValid(string token);
        string? GetNameFromToken(string jwtToken);
    }
}
