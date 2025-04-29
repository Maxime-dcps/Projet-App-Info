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
                                    tag_id AS TagId,
                                    tag_name AS TagName
                                FROM tags
                                WHERE listing_id = @Id
                                """;

            var result = await connection.QueryAsync<TagViewModel>(sql, new { Id = id });

            return result.ToList();
        }
    }
}
