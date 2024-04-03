using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Domain.Interfaces.Persistence
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetLogsAsync(GetLogDto getLog);
        Task<Log?> CreateLogAsync(LogDto logDto);
        Task<Log?> UpdateLogAsync(LogDto logDto);

    }
}
