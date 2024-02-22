using Dapper;
using Dashboard.Domain.Variables;
using MediaLogger.Domain.Entities.Business;
using MediaLogger.Domain.Exceptions;
using MediaLogger.Domain.Variables;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace MediaLogger.Persistence.SQLServer
{
    public class UserRepository 
    {
        private readonly string? _dataBase;

        public UserRepository(IConfiguration configuration)
        {
            _dataBase = configuration.GetSection(AppSettings.ConnetionDashboardSQL).Value;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                using var conn = new SqlConnection(_dataBase);
                IEnumerable<User> users = await conn.QueryAsync<User>(Procedures.SECURITY_SP_USERS_GETALL, null, commandType: CommandType.StoredProcedure);

                await conn.CloseAsync();
                await conn.DisposeAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw new DbException(ex.Message, ex);
            }
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            try
            {
                var args = new
                {
                    id
                };
                using var conn = new SqlConnection(_dataBase);
                User? user = (await conn.QueryAsync<User>(Procedures.SECURITY_SP_USERS_GETBYID, args, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                await conn.CloseAsync();
                await conn.DisposeAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new DbException(ex.Message,ex);
            }
        }

        public async Task<string?> GetUserPassword(string? document)
        {
            try
            {
                var args = new
                {
                    document
                };
                using var conn = new SqlConnection(_dataBase);
                string? pwd = (await conn.QueryAsync<string>(Procedures.SECURITY_SP_USERS_GETPWD, args, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                await conn.CloseAsync();
                await conn.DisposeAsync();
                return pwd;
            }
            catch (Exception ex)
            {
                throw new DbException(ex.Message,ex);
            }
        }

    }
}
