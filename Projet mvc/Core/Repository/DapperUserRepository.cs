using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Infrastructure;
using Dapper;
using Projet_mvc.Models.User;

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

        public async Task<User> GetByIdAsync(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = "SELECT * FROM users WHERE user_id = @UserId";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = userId });
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();



            const string sql = @"
                                UPDATE users
                                SET
                                username = @Username,
                                email = @Email,
                                password_hash = @Password_Hash,
                                salt = @Salt
                                WHERE user_id = @User_Id";

            var parameters = new
            {
                user.Username,
                user.Email,
                user.Password_Hash,
                user.Salt,
                user.User_Id
            };

            var rows = await connection.ExecuteAsync(sql, parameters);

            // Si la mise à jour a eu lieu, retourne l'utilisateur, sinon null
            return rows > 0 ? user : null;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = "DELETE FROM users WHERE user_id = @UserId";

            var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId });

            return rowsAffected > 0;
        }



    }

}
