using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Infrastructure;
using Dapper;

namespace Projet_mvc.Core.Repository
{
    public class DapperUserRepository : IUserRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperUserRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                           INSERT INTO users (username, email, password_hash, salt, role)
                           VALUES (@Username, @Email, @Password_Hash, @Salt, @Role)
                           RETURNING user_id
                           """;

            return await connection.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task<bool> UsernameExist(string username)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            return await connection.ExecuteScalarAsync<bool>("""
                                                         SELECT EXISTS (SELECT * FROM users WHERE username = @username)
                                                         """, new { username });
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = "SELECT * FROM users WHERE username = @username";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { username });
        }
    }

}
