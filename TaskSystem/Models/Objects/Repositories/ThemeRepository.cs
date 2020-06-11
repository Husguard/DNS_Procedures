using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Репозиторий получения и добавления тем
    /// </summary>
    public class ThemeRepository : IThemeRepository
    {
        private readonly IConnectionDb _db;

        public ThemeRepository(IConnectionDb db)
        {
            _db = db;
        }
        /// <summary>
        /// Метод добавления новой темы по названию
        /// </summary>
        /// <param name="name"></param>
        public void AddTheme(string name)
        {
            _db.ExecuteNonQuery(
               "TaskProcedureAddTheme",
               new SqlParameter("@Name", name)
               );
        }
        /// <summary>
        /// Метод получения всех тем
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Theme> GetAllThemes()
        {
            return _db.ExecuteReaderGetList<Theme>(
                "TaskProcedureGetAllThemes",
                ThemeFromReader);
        }
        /// <summary>
        /// Получение тем, которые начинаются с введенной строки
        /// </summary>
        /// <param name="name">Часть названия темы</param>
        /// <returns></returns>
        public IEnumerable<Theme> GetThemesByName(string name)
        {
            return _db.ExecuteReaderGetList<Theme>(
                "TaskProcedureGetThemesByName",
                ThemeFromReader);
        }
        /// <summary>
        /// Метод создания объекта из данных от БД
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Theme ThemeFromReader(IDataReader reader)
        {
            return new Theme()
            {
                Id = (int)reader["ID"],
                Name = (string)reader["Name"]
            };
        }
    }
}
