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

        public async Task<List<ImageViewModel>> GetImagesByIdAsync(int id)
        {
            using var connection = await _dbConnectionProvider.CreateConnection();
            const string sql = """
                                SELECT 
                                    image_id AS ImageId,
                                    file_path AS FilePath,
                                    alt_text AS AltText,
                                    image_order AS Order
                                FROM images
                                WHERE listing_id = @Id
                                ORDER BY image_order;
                                """;

            var result = await connection.QueryAsync<ImageViewModel>(sql, new { Id = id });

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
    }
}
