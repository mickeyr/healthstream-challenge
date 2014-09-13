using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace HealthStream.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDbCommand CreateCommand();
        void SaveChanges();
    }
}
