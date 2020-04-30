using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer.Repository
{
    public class SqlServerRepository : IDisposable
    {

        private SqlConnection _connection = null;
        private string _connectionStringId;
        private string _connectionString;

        public SqlServerRepository()
            : this(connectionStringId: "EPROCSERVER")
        {

        }

        public SqlServerRepository(string connectionStringId)
        {
            _connectionStringId = connectionStringId;
        }

        public virtual string ConnectionStringId
        {
            get
            {
                return _connectionStringId;
            }
        }

        protected SqlConnection Database
        {
            get
            {
                if (_connection == null)
                {
                    if (string.IsNullOrEmpty(_connectionString))
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringId].ConnectionString;
                    }

                    _connection = new SqlConnection(_connectionString);
                }

                return _connection;
            }
        }

        public SqlServerRepository UseConnectionStringId(string connectionStringId)
        {
            _connectionStringId = connectionStringId;

            return this;
        }

        public SqlServerRepository UseConnectionString(string connectionString)
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
