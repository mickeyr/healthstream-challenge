using System;
using System.Data;

namespace HealthStream.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDbCommand CreateCommand();
        void SaveChanges();
    }
}
