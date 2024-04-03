using MediaLogger.Domain.Variables;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Variables;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MediaLogger.Domain.DTOs.Business;
using MediaLogger.Domain.Interfaces.Persistence;

namespace MediaLogger.Persistence.SQLServer.Business
{
    public class LogRepository: ILogRepository
    {
        private readonly string? _dataBase;
        public LogRepository(IConfiguration configuration) 
        {
            _dataBase = configuration.GetSection(AppSettings.ConnetionLogsSQL).Value;
        }
        public async Task<IEnumerable<Log>> GetLogsAsync(GetLogDto getLog)
        {
            using var conn = new SqlConnection(_dataBase);
            IEnumerable<Log> logs = await conn.QueryAsync<Log>(Procedures.BUSINESS_SP_GETLOGS, param: getLog, commandType: CommandType.StoredProcedure);

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return logs;
        }

        public async Task<Log?> CreateLogAsync(LogDto logDto)
        {
            using var conn = new SqlConnection(_dataBase);
            Log? logR = (await conn.QueryAsync<Log>(Procedures.BUSINESS_SP_CREATELOGS, param: logDto, commandType: CommandType.StoredProcedure)).FirstOrDefault();

            await conn.CloseAsync();
            await conn.DisposeAsync();

            return logR;
        }

        public async Task<Log?> UpdateLogAsync(LogDto logDto)
        {
            using var conn = new SqlConnection(_dataBase);
            Log? logR = (await conn.QueryAsync<Log>(Procedures.BUSINESS_SP_UPDATELOGS, param: logDto, commandType: CommandType.StoredProcedure)).FirstOrDefault();

            await conn.CloseAsync();
            await conn.DisposeAsync();

            return logR;
        }
    }
}
