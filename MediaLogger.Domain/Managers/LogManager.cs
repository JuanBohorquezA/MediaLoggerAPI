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
        public Task<Log?> LogService(SaveLogDto reqLog, int IdPaypad, string? Paypad)
        {
            if (string.IsNullOrEmpty(reqLog.Content)) throw new Exception(ResponseMessage.EMPTYFIELDS);
            if (!Enum.GetNames(typeof(ETypeLogReq)).Any(x => x == reqLog.Logtype)) throw new Exception(ResponseMessage.Error($"'{reqLog.Logtype}' doesn't exist in the enumerable"));

            var logAction = string.IsNullOrEmpty(reqLog.idLog) ? _logBL.InsertLog(reqLog, IdPaypad, Paypad) : _logBL.UpdateLog(reqLog, IdPaypad, Paypad);
            if (logAction == null) throw new Exception(ResponseMessage.Error($"log could not been inserted"));
            return logAction;
        }
    }
}
