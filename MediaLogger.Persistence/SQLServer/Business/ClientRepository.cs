using Dapper;
using Dashboard.Domain.Variables;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Variables;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MediaLogger.Persistence.SQLServer
{
    public class ClientRepository 
    {
        private readonly string? _dataBase;

        public ClientRepository(IConfiguration configuration)
        {
            _dataBase = configuration.GetSection(AppSettings.ConnetionDashboardSQL).Value;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            using var conn = new SqlConnection(_dataBase);
            IEnumerable<Client> client = await conn.QueryAsync<Client>(Procedures.BUSINESS_SP_CLIENTS_GETALL, null, commandType: CommandType.StoredProcedure);

            await conn.CloseAsync();
            await conn.DisposeAsync();
            return client;
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return (await GetAllAsync()).Where(x => x.ID == id).FirstOrDefault();
        }

       
    }
}
