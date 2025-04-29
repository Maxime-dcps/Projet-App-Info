using Dapper;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Infrastructure;
using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public class DapperListingRepository : IListingRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;


        public DapperListingRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<List<ListingSummaryViewModel>> GetRecentListingsAsync(int count)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                                SELECT 
                                    l.listing_id AS ListingId,
                                    l.title AS Title,
                                    l.price AS Price,
                                    img.file_path AS PrimaryImageUrl,
                                    img.alt_text AS PrimaryImageAlt
                                FROM listings l
                                LEFT JOIN (
                                    SELECT DISTINCT ON (listing_id)
                                        listing_id,
                                        file_path,
                                        alt_text
                                    FROM images
                                    ORDER BY listing_id, image_order
                                ) img ON l.listing_id = img.listing_id
                                WHERE l.is_available = true
                                ORDER BY l.creation_date DESC
                                LIMIT @Count;
                                """;

            var result = await connection.QueryAsync<ListingSummaryViewModel>(sql, new { Count = count });

            foreach (var summary in result)
            {
                if (string.IsNullOrEmpty(summary.PrimaryImageUrl))
                {
                    summary.PrimaryImageUrl = "/images/placeholder.png";
                    summary.PrimaryImageAlt = "Image non disponible";
                }
                else if (string.IsNullOrEmpty(summary.PrimaryImageAlt))
                {
                    summary.PrimaryImageAlt = summary.Title;
                }
            }

            return result.ToList();
        }
    }
}
