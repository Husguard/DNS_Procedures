using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Interfaces
{
    interface IRepository
    {
        T Create<T>(IDataReader reader);
    }
}
