using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Application
{
    public interface ILogBL
    {
        public Task<Log?> InsertLog(SaveLogDto reqLog, int IdPaypad, string? Paypad);
        public Task<List<string?>?> GetLogs(GetLogDto getLog);
    }
}
