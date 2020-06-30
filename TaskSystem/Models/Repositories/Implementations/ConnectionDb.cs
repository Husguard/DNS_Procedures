using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskSystem.Models.Interfaces;
using Microsoft.Extensions.Options;
using TaskSystem.Models.Options;

namespace TaskSystem.Models
{
    /// <summary>
    /// Класс подключения к БД
    /// </summary>
    public class ConnectionDb : IConnectionDb
    {
        private readonly string _connectionName;
        /// <summary>
        /// Инициализация класса
        /// </summary>
        /// <param name="options">Настройки подключения</param>
        public ConnectionDb(IOptions<ConnectionStringOptions> options)
        {
            _connectionName = options.Value.DefaultConnection;
        }

        /// <summary>
        /// Метод исполнения процедуры БД, требующий чтения результата и создания коллекции объектов по делегату
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого экземпляра</typeparam>
        /// <param name="storedProcedure">Процедура, которую необходимо исполнить</param>
        /// <param name="readerFunc">Метод создания экземпляра</param>
        /// <param name="args">Параметры процедуры</param>
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

        /// <summary>
        /// Метод исполнения процедуры, не возвращающий результат от БД
        /// </summary>
        /// <param name="storedProcedure">Процедура, которую необходимо исполнить</param>
        /// <param name="args">Параметры процедуры</param>
        /// <returns> Количество затронутых записей </returns>
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

        /// <summary>
        /// Метод исполнения процедуры БД, требующий чтения результата и создания одного объекта по делегату
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого экземпляра</typeparam>
        /// <param name="storedProcedure">Процедура, которую необходимо исполнить</param>
        /// <param name="readerFunc">Метод создания экземпляра</param>
        /// <param name="args">Параметры процедуры</param>
        public T ExecuteReaderGetSingle<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args) where T : class
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
                while (reader.Read())
                {
                    return readerFunc(reader);
                }
                return default;
            }
        }
    }
}
