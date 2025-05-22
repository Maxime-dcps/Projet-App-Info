using Dapper;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Infrastructure;

namespace Projet_mvc.Core.Repository
{
    public class DapperFavoriteRepository : IFavoriteRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public DapperFavoriteRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task AddAsync(int userId, int listingId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                               INSERT INTO favorites (user_id, listing_id)
                               VALUES (@UserId, @ListingId)
                               ON CONFLICT (user_id, listing_id) DO NOTHING;
                               """;

            await connection.ExecuteAsync(sql, new { UserId = userId, ListingId = listingId });
        }

        public async Task RemoveAsync(int userId, int listingId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                               DELETE FROM favorites
                               WHERE user_id = @UserId AND listing_id = @ListingId;
                               """;

            await connection.ExecuteAsync(sql, new { UserId = userId, ListingId = listingId });
        }

        public async Task<bool> ExistsAsync(int userId, int listingId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                               SELECT 1
                               FROM favorites
                               WHERE user_id = @UserId AND listing_id = @ListingId;
                               """;

            var exists = await connection.QueryFirstOrDefaultAsync<int?>(sql, new { UserId = userId, ListingId = listingId });
            return exists.HasValue;
        }

        public async Task<List<Listing>> GetFavoritesForUserAsync(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                               SELECT 
                                      l.listing_id AS Id,
                                      l.title AS Title,
                                      l.description AS Description,
                                      l.price AS Price,
                                      l.is_available AS IsAvailable,
                                      l.creation_date AS CreationDate,
                                      l.user_id AS UserId
                               FROM listings l
                               INNER JOIN favorites f ON l.listing_id = f.listing_id
                               WHERE f.user_id = @UserId;
                               """;

            var result = await connection.QueryAsync<Listing>(sql, new { UserId = userId });
            return result.ToList();
        }




    }
}
