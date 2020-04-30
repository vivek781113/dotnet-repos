using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace DataAccessLayer.Repository
{
    public class OracleServerRepository : IDisposable
    {
        private OracleConnection _connection = null;
        private string _connectionStringId;
        private string _connectionString;

        public OracleServerRepository() : this(connectionStringId: "EASYBUYSERVER")
        {

        }
        public OracleServerRepository(string connectionStringId)
        {
            _connectionStringId = connectionStringId;
        }

        public virtual string ConnectionStringId
        {
            get { return _connectionStringId; }
        }

        protected OracleConnection Database
        {
            get
            {
                if (_connection == null)
                {
                    if (string.IsNullOrWhiteSpace(_connectionString))
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringId].ConnectionString;
                    }

                    _connection = new OracleConnection(_connectionString);
                }
                return _connection;
            }
        }

        public OracleServerRepository UseConnectionStringId(string connectionStringId)
        {
            _connectionStringId = connectionStringId;

            return this;
        }

        public OracleServerRepository UseConnectionString(string connectionString)
        {
            _connectionString = connectionString;

            return this;
        }

        public virtual void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
