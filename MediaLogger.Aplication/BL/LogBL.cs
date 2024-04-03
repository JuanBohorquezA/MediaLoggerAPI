using MediaLogger.Domain;
using MediaLogger.Domain.DTOs;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Enumerables;
using MediaLogger.Domain.Interfaces.Application;
using MediaLogger.Domain.Interfaces.Persistence;
using Microsoft.Extensions.Configuration;


namespace MediaLogger.Aplication.BL
{
    public class LogBL: ILogBL
    {
        private readonly IConfiguration _configuration;
        private readonly ILogRepository _logRepository;
        public LogBL(IConfiguration configuration, ILogRepository logRepository) 
        {
            _configuration = configuration;
            _logRepository = logRepository;
        }
        public async Task<Log?> InsertLog(SaveLogDto reqLog,int IdPaypad, string? Paypad)
        {
            var logDto = new LogDto
            {
                IdPaypad = IdPaypad,
                LogType = reqLog.Logtype.ToString(),
                Paypad = Paypad,
                Log = reqLog.Content,
                DateCreated = DateTime.Now,
            };
            Log? logInserted = await _logRepository.CreateLogAsync(logDto);
            return logInserted;
        }
        public async Task<Log?> UpdateLog(SaveLogDto reqLog, int IdPaypad, string? Paypad)
        {
            long idLog = long.TryParse(reqLog.idLog, out long parsedIdLog) ? parsedIdLog : -1;
            var logDto = new LogDto
            {
                IdLog = idLog,
                IdPaypad = IdPaypad,
                LogType = reqLog.Logtype.ToString(),
                Paypad = Paypad,
                Log = reqLog.Content,
                DateCreated = DateTime.Now,
            };
            Log? logInserted = await _logRepository.UpdateLogAsync(logDto);
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
