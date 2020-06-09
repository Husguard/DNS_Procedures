using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Interfaces
{
    public interface IConnectionDb
    {
        /// <summary>
        /// Метод исполнения процедуры БД, требующий чтения результата и создания объекта по делегату
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure"></param>
        /// <param name="readerFunc"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        List<T> ExecuteReader<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args);
        /// <summary>
        /// Метод исполнения процедуры, не возвращающий результат от БД
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="args"></param>
        void ExecuteNonQuery(string storedProcedure, params SqlParameter[] args);
    }
}
