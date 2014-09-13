using System.Configuration;

namespace HealthStream.Data
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(ConnectionStringSettings connectionString);
    }
}
