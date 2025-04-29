using Npgsql;
using System.Data;

namespace Projet_mvc.Core.Infrastructure
{
    internal class PGSqlDbConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public PGSqlDbConnectionProvider(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }
        public async Task<IDbConnection> CreateConnection()
        {
            //var connection = new NpgsqlConnection("Host=localhost;Database=project_app_info;Username=anonyme;Password=anonyme");

            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return connection;
        }
    }
}
