using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Repositories.Interfaces;

namespace TaskSystem.Models.Repositories.Implementations
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
        public IEnumerable<Theme> GetThemesByName(string name)
        {
            return _db.ExecuteReaderGetList<Theme>(
                "TaskProcedureGetThemesByName",
                ThemeFromReader,
                new SqlParameter("@Name", name));
        }

        /// <summary>
        /// Метод создания объекта из данных от БД
        /// </summary>
        /// <param name="reader">Класс чтения потоков данных из БД</param>
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
