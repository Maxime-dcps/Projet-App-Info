using System.Data;

namespace Projet_mvc.Core.Infrastructure
{
    public interface IDbConnectionProvider
    {
        public Task<IDbConnection> CreateConnection();
    }
}
