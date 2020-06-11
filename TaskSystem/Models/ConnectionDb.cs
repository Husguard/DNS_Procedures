using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Objects.Repositories;
using TaskSystem.Models.Interfaces;
using System.Globalization;
using Microsoft.Extensions.Options;
using TaskSystem.Models.Options;

namespace TaskSystem.Models
{
    public class ConnectionDb : IConnectionDb
    {
        private readonly string _connectionName;
        public ConnectionDb(ConnectionOptions options)
        {
            _connectionName = options.DefaultConnection;
        }
        // централизовать подключение для reader и nonquery
        public List<T> ExecuteReader<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(_connectionName))
            {
                connection.Open();
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var dbParam in args)
                {
                    command.Parameters.Add(dbParam);
                }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(readerFunc(reader));
                }
                return result;
            }
        }
        public void ExecuteNonQuery(string storedProcedure, params SqlParameter[] args)
        {
            using (var connection = new SqlConnection(_connectionName))
            {
                connection.Open();
                var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (var dbParam in args)
                {
                    command.Parameters.Add(dbParam);
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
