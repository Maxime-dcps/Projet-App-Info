using Dapper;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Infrastructure;
using Projet_mvc.Models;
using Projet_mvc.Models.Listing;

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

            SetDefaultImageProperties(result);

            return result.ToList();
        }

        private static void SetDefaultImageProperties(IEnumerable<ListingSummaryViewModel> result)
        {
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
        }

        public async Task<List<ListingSummaryViewModel>> GetPopularListingsAsync(int count)
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
                                ORDER BY popularity(l.listing_id) DESC, l.creation_date DESC
                                LIMIT @Count;
                                """;

            var result = await connection.QueryAsync<ListingSummaryViewModel>(sql, new { Count = count });

            SetDefaultImageProperties(result);

            return result.ToList();
        }

        public async Task<List<ListingSummaryViewModel>> GetAllListingsAsync()
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
                                ORDER BY l.creation_date DESC;
                               """;

            var result = await connection.QueryAsync<ListingSummaryViewModel>(sql);

            SetDefaultImageProperties(result);

            return result.ToList();
        }

        public async Task<int> CreateListingAsync(Listing listing)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                                INSERT INTO listings (title, description, price, user_id)
                                VALUES (@Title, @Description, @Price, @UserId)
                                RETURNING listing_id;
                               """;

            return await connection.ExecuteScalarAsync<int>(sql, listing);
        }

        public async Task<IEnumerable<Listing>> GetListingsByUserIdAsync(int userId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            var sql = """
                        SELECT 
                            l.listing_id AS Id,
                            l.title AS Title,
                            l.description AS Description,
                            l.price AS Price,
                            l.is_available AS IsAvailable,
                            l.creation_date AS CreationDate,
                            u.user_id AS UserId,
                            u.username AS AuthorName
                        FROM listings l
                        LEFT JOIN users u ON l.user_id = u.user_id
                        WHERE l.user_id = @UserId
                      """;
            return await connection.QueryAsync<Listing>(sql, new { UserId = userId });
        }



        public async Task<Listing?> GetListingByIdAsync(int id)
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
                                    u.user_id AS UserId,
                                    u.username AS AuthorName
                                FROM listings l
                                LEFT JOIN users u ON l.user_id = u.user_id
                                WHERE l.listing_id = @Id AND l.is_available = true;
                                """;
            var result = await connection.QueryFirstOrDefaultAsync<Listing>(sql, new { Id = id });
            return result;

        }

        public async Task<int> UpdateListingAsync(Listing listing)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                UPDATE listings
                                SET title = @Title,
                                    description = @Description,
                                    price = @Price,
                                    is_available = @IsAvailable
                                WHERE listing_id = @Id;
                               """;
            var rows = await connection.ExecuteAsync(sql, listing);

            return rows;
        }

        public async Task<bool> DeleteListingAsync(int id)
        {
            // Soft delete: set is_available to false

            // TODO: hard delete after a certain period

            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                UPDATE listings
                                SET is_available = false
                                WHERE listing_id = @Id;
                               """;
            var rows = await connection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public  async Task<List<ListingSummaryViewModel>> GetFilteredListingsAsync(ListingFilterViewModel filter)
        {
            var parameters = new DynamicParameters();

            using var connection = await _dbConnectionProvider.CreateConnection();

            var sql = """
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
                        """;
            if (filter.MinPrice.HasValue)
            {
                sql += " AND l.price >= @MinPrice ";
                parameters.Add("MinPrice", filter.MinPrice.Value);
            }
            if (filter.MaxPrice.HasValue)
            {
                sql += " AND l.price <= @MaxPrice ";
                parameters.Add("MaxPrice", filter.MaxPrice.Value);
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                sql += " AND l.title ILIKE '%' || @SearchTerm || '%' ";
                parameters.Add("SearchTerm", filter.SearchTerm);
            }

            if (filter.SelectedTagIds != null && filter.SelectedTagIds.Any())
            {
                // Ensure the tag matches the of selected tags

                sql += """

                    AND l.listing_id IN (
                        SELECT lt.listing_id
                        FROM listing_tags lt
                        WHERE tag_id = ANY(@SelectedTagIds)
                        GROUP BY lt.listing_id
                        HAVING COUNT(DISTINCT lt.tag_id) = @TagCount

                    )
                    """;
                parameters.Add("SelectedTagIds", filter.SelectedTagIds);
                parameters.Add("TagCount", filter.SelectedTagIds.Count);
            }

            if (filter.SortOrder != null)
            {
                switch (filter.SortOrder)
                {
                    case "price_asc":
                        sql += " ORDER BY l.price ASC";
                        break;
                    case "price_desc":
                        sql += " ORDER BY l.price DESC";
                        break;
                    case "date_asc":
                        sql += " ORDER BY l.creation_date ASC";
                        break;
                    case "date_desc":
                        sql += " ORDER BY l.creation_date DESC";
                        break;
                    case "popular":
                        // Assuming a popularity function exists that returns
                        // SELECT count(*) INTO v_count FROM favorites WHERE listing_id = p_listing_id;
                        sql += " ORDER BY popularity(l.listing_id) DESC";
                        break;
                    default:
                        sql += " ORDER BY l.creation_date DESC";
                        break;
                }
            }
            else sql += " ORDER BY l.creation_date DESC";

            var result = await connection.QueryAsync<ListingSummaryViewModel>(sql, parameters);

            SetDefaultImageProperties(result);

            return result.ToList();
        }
    }
}
