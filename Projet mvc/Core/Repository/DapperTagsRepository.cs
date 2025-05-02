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
    }
}
