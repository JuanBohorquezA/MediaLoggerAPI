using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces.Application;
using MediaLogger.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Managers
{
    public class LogManager : ILogManager
    {
        private readonly ILogBL _logBL;
        public LogManager(ILogBL logBL)
        {
            _logBL = logBL;
        }
        public async Task<Log?> LogService(SaveLogDto reqLog, int IdPaypad, string? Paypad)
        {
            var logAction = string.IsNullOrEmpty(reqLog.idLog) ? await _logBL.InsertLog(reqLog, IdPaypad, Paypad) : await _logBL.UpdateLog(reqLog, IdPaypad, Paypad);
            return logAction == null? throw new Exception(ResponseMessage.Error($"log could not been inserted")): logAction;
        }
    }
}
