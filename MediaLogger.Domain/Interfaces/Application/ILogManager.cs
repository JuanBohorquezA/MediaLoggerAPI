using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Application
{
    public interface ILogManager
    {
        public Task<Log?> LogService(SaveLogDto reqLog, int IdPaypad, string? Paypad);
    }
}
