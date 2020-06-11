using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Метод создания объекта в репозитории
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        T Create<T>(IDataReader reader);
    }
}
