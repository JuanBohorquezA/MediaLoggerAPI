﻿using Dapper;
using Dashboard.Domain.Variables;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Variables;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MediaLogger.Persistence.SQLServer
{
    public class PayPadRepository
    {
        private readonly string? _dataBase;

        public PayPadRepository(IConfiguration configuration)
        {
            _dataBase = configuration.GetSection(AppSettings.ConnetionSQL).Value;
        }

        public async Task<IEnumerable<PayPad>> GetAllAsync()
        {
            using var conn = new SqlConnection(_dataBase);

            IEnumerable<PayPad> paypad = await conn.QueryAsync<PayPad>(Procedures.BUSINESS_SP_PAYPAD_GETALL, null, commandType: CommandType.StoredProcedure);

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return paypad;
        }
        public async Task<PayPad?> GetByIdAsync(int id)
        {
            return (await GetAllAsync()).Where(x => x.ID == id).FirstOrDefault();
        }


        public async Task<string?> GetPaypadPasswordAsync(int id)
        {
            var args = new
            {
                Id = id
            };
            using var conn = new SqlConnection(_dataBase);
            
            string? password = (await conn.QueryAsync<string>(Procedures.BUSINESS_SP_PAYPAD_GETPWD, args, commandType: CommandType.StoredProcedure)).FirstOrDefault();

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return password;
        }



       
    }
}
