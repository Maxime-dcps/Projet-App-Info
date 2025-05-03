using Dapper;
using Projet_mvc.Core.Infrastructure;
using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public class DapperTagsRepository : ITagRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        public DapperTagsRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }
        public async Task<List<TagViewModel>> GetTagsByIdAsync(int id)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                SELECT 
                                    l.tag_id AS Id,
                                    t.label AS Label
                                FROM listing_tags l
                                INNER JOIN tags t ON l.tag_id = t.tag_id
                                WHERE listing_id = @Id
                                """;

            var result = await connection.QueryAsync<TagViewModel>(sql, new { Id = id });

            return result.ToList();
        }

        public async Task<List<TagViewModel>> GetAllTagsAsync()
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                                SELECT 
                                    tag_id AS Id,
                                    label AS Label
                                FROM tags
                                """;

            var result = await connection.QueryAsync<TagViewModel>(sql);

            return result.ToList();
        }

        public async Task AddTagsToListingAsync(int newListingId, List<int> selectedTagsIds)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                INSERT INTO listing_tags (listing_id, tag_id)
                                VALUES (@ListingId, @TagId)
                                """;
            foreach (var tagId in selectedTagsIds)
            {
                await connection.ExecuteAsync(sql, new { ListingId = newListingId, TagId = tagId });
            }

            // Alternatively, you can use a single query with multiple values

            //var parameters = selectedTagsIds.Select(tagId => new { ListingId = newListingId, TagId = tagId }).ToList();

            //await connection.ExecuteAsync(sql, parameters);
        }

        public async Task UpdateTagsToListingAsync(int listingId, List<int> selectedTagsIds)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                DELETE FROM listing_tags
                                WHERE listing_id = @ListingId
                                """;
            await connection.ExecuteAsync(sql, new { ListingId = listingId });
            
            AddTagsToListingAsync(listingId, selectedTagsIds).Wait();
        }
    }
}
