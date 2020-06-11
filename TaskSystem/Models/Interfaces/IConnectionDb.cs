using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Interfaces
{
    /// <summary>
    /// Интерфейс чтения данных из базы данных
    /// </summary>
    public interface IConnectionDb
    {
        /// <summary>
        /// Метод исполнения процедуры БД, требующий чтения результата и создания коллекции объектов по делегату
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого экземпляра</typeparam>
        /// <param name="storedProcedure">Процедура, которую необходимо исполнить</param>
        /// <param name="readerFunc">Метод создания экземпляра</param>
        /// <param name="args">Параметры процедуры</param>
        List<T> ExecuteReaderGetList<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args);

        /// <summary>
        /// Метод исполнения процедуры, не возвращающий результат от БД
        /// </summary>
        /// <param name="storedProcedure">Процедура, которую необходимо исполнить</param>
        /// <param name="args">Параметры процедуры</param>
        void ExecuteNonQuery(string storedProcedure, params SqlParameter[] args);
        /// <summary>
        /// Метод исполнения процедуры БД, требующий чтения результата и создания одного объекта по делегату
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого экземпляра</typeparam>
        /// <param name="storedProcedure">Процедура, которую необходимо исполнить</param>
        /// <param name="readerFunc">Метод создания экземпляра</param>
        /// <param name="args">Параметры процедуры</param>
        T ExecuteReaderGetSingle<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args);
    }
}
