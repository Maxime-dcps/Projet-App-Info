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




    }
}
