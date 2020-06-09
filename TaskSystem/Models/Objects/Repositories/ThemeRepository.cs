using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly IConnectionDb _db;

        public ThemeRepository(IConnectionDb db)
        {
            _db = db;
        }
        public void AddTheme(string name)
        {
            _db.ExecuteNonQuery(
               "TaskProcedureAddTheme",
               new SqlParameter("@Name", name)
               );
        }
        public IEnumerable<Theme> GetAllThemes()
        {
            return _db.ExecuteReader<Theme>(
                "TaskProcedureGetTaskByID",
                (reader) => CreateTheme(reader));
        }
        public IEnumerable<Theme> GetThemesByName(string name)
        {
            return _db.ExecuteReader<Theme>(
                "TaskProcedureGetTaskByID",
                (reader) => CreateTheme(reader),
                new SqlParameter("@Name", name));
        }
        private Theme CreateTheme(IDataReader reader)
        {
            return new Theme()
            {
                ID = (int)reader["ID"],
                Name = (string)reader["Name"]
            };
        }
    }
}
