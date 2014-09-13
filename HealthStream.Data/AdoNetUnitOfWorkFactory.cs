using System;
using System.Configuration;
using System.Data.Common;

namespace HealthStream.Data
{
    public class AdoNetUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create(ConnectionStringSettings  connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString", "The connection string can not be null.");
            }

            if (String.IsNullOrWhiteSpace(connectionString.ProviderName))
            {
                throw new ArgumentException("The ProviderName must be defined on the connection string passed in.", "connectionString");
            }

            var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);
            var connection = factory.CreateConnection();
            if (connection == null)
            {
                throw new ArgumentException("Unable to connect to the database.", "connectionString");
            }

            connection.ConnectionString = connectionString.ConnectionString;
            connection.Open();
            return new AdoNetUnitOfWork(connection);
        }
    }
}
