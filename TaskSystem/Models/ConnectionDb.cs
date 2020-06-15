using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskSystem.Models.Interfaces;
using Microsoft.Extensions.Options;
using TaskSystem.Models.Options;

namespace TaskSystem.Models
{
    public class ConnectionDb : IConnectionDb
    {
        private readonly string _connectionName;
        public ConnectionDb(IOptions<ConnectionStringOptions> options)
        {
            _connectionName = options.Value.DefaultConnection;
        }
        // централизовать подключение для reader и nonquery
        public List<T> ExecuteReaderGetList<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args)
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
        public int ExecuteNonQuery(string storedProcedure, params SqlParameter[] args)
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
                return command.ExecuteNonQuery();
            }
        }
        public T ExecuteReaderGetSingle<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args)
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
                var reader = command.ExecuteReader();
                reader.Read();
                return readerFunc(reader);
            }
        }
    }
}
