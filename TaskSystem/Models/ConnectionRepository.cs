using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TaskSystem.Models
{
    public class ConnectionRepository
    {
        private readonly string connectionName;
        public ConnectionRepository()
        {
            connectionName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
