using Projet_mvc.Core.Infrastructure;
using Projet_mvc.Models;
using Dapper;
using Projet_mvc.Core.Domain;


namespace Projet_mvc.Core.Repository
{
    public class DapperImageRepository : IImageRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;


        public DapperImageRepository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public async Task<List<ImageViewModel>> GetImagesByIdAsync(int listingId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                SELECT 
                                    image_id AS ImageId,
                                    file_path AS FilePath,
                                    alt_text AS AltText,
                                    image_order AS Order
                                FROM images
                                WHERE listing_id = @listingId
                                ORDER BY image_order;
                                """;

            var result = await connection.QueryAsync<ImageViewModel>(sql, new { listingId });

            return result.ToList();
        }

        public async Task AddImageAsync(Image model)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                INSERT INTO images (listing_id, file_path, alt_text, image_order, upload_date)
                                VALUES (@ListingId, @FilePath, @AltText, @ImageOrder, @UploadDate);
                                """;
            await connection.ExecuteAsync(sql, model);
        }

        public async Task<int> GetLastImageOrderAsync(int listingId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                                SELECT COALESCE(MAX(image_order), 0) 
                                FROM images 
                                WHERE listing_id = @listingId;
                                """;
            var result = await connection.QuerySingleAsync<int>(sql, new { listingId });

            return result;
        }

        public async Task<ImageViewModel?> GetImageByImageIdAsync(int imageId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                                SELECT 
                                    image_id AS ImageId,
                                    file_path AS FilePath,
                                    alt_text AS AltText,
                                    image_order AS Order
                                FROM images
                                WHERE image_id = @imageId;
                                """;

            var result = await connection.QuerySingleOrDefaultAsync<ImageViewModel>(sql, new { imageId });

            return result;
        }

        public async Task DeleteImageAsync(int imageId)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();

            const string sql = """
                                DELETE FROM images
                                WHERE image_id = @imageId;
                                """;

            await connection.ExecuteAsync(sql, new { imageId });
        }
    }
}
