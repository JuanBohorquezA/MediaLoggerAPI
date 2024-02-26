using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces;
using MediaLogger.Persistence.SQLServer;
using MediaLogger.Persistence.SQLServer.Business;
using Microsoft.Extensions.Configuration;


namespace MediaLogger.Aplication.BL
{
    public class LogBL: IlogsBL
    {
        private readonly IConfiguration _configuration;
        private readonly LogRepository _logRepository;
        public LogBL(IConfiguration configuration, LogRepository logRepository) 
        {
            _configuration = configuration;
            _logRepository = logRepository;
        }
        public async Task<Log?> InsertLog(SaveLogDto reqLog,int IdPaypad, string? Paypad)
        {
            if (string.IsNullOrEmpty(reqLog.Content)) throw new Exception(ResponseMessage.EMPTYFIELDS);
            if(!Enum.GetNames(typeof(ETypeLogReq)).Any(x => x == reqLog.Logtype)) throw new Exception(ResponseMessage.Error($"'{reqLog.Logtype}' doesn't exist in the enumerable"));

            var logDto = new LogDto
            {
                IdPaypad = IdPaypad,
                LogType = reqLog.Logtype.ToString(),
                Paypad = Paypad,
                Log = reqLog.Content,
                DateCreated = DateTime.Now,
            };
            Log? logInserted = await _logRepository.CreateLogAsync(logDto);
            if (logInserted == null) throw new Exception(ResponseMessage.Error($"log could not inserted"));
            return logInserted;
        }

        public async Task<List<string?>?>GetLogs(GetLogDto getLog)
        {
            if(getLog == null) throw new Exception(ResponseMessage.EMPTYFIELDS);
            if (getLog.StartDate > getLog.FinalDate) throw new Exception(ResponseMessage.Error($"range of data is not valid"));
            IEnumerable<Log> logs = await _logRepository.GetLogsAsync(getLog);
            if (logs.Count() <= 0) throw new Exception(ResponseMessage.Error($"consult date has not logs"));
            var log = logs.Select(log=>log.LOG).ToList();
            if (log.Count() <= 0) throw new Exception("something wrong has happened");
            return log;
            
        }
    }
}
