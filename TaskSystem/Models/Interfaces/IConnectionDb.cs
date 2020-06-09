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
        List<T> ExecuteReader<T>(string storedProcedure, Func<IDataReader, T> readerFunc, params SqlParameter[] args);
        void ExecuteNonQuery(string storedProcedure, params SqlParameter[] args);
    }
}
