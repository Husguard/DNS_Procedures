using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Objects.Repositories;
using TaskSystem.Models.Interfaces;
using System.Globalization;

namespace TaskSystem.Models
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly string connectionName;
        private CommentRepository commentRepository;
        private TaskRepository taskRepository;
        private ThemeRepository themeRepository;
        public ConnectionRepository()
        {
            connectionName = "Server=test-itweb-sql.partner.ru;Database=Shevelev;Trusted_Connection=True;";
            themeRepository = new ThemeRepository();
            commentRepository = new CommentRepository();
            taskRepository = new TaskRepository();
            themeRepository.Themes = DownloadThemes();
        }
        public List<Theme> DownloadThemes()
        {
            List<Theme> themes = new List<Theme>();
            using (SqlConnection connection = new SqlConnection(connectionName))
            {
                connection.Open();
                string sqlExpression = "EXEC TaskProcedureGetAllThemes";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            themes.Add(
                                new Theme()
                                {
                                    ID = Convert.ToInt32(reader["Id"]),
                                    Name = Convert.ToString(reader["Name"])
                                });
                        }
                    }
                }
                connection.Close();
            }
            return themes;
        }
        public List<Task> DownloadTasks()
        {
            List<Task> tasks = new List<Task>();
            using (SqlConnection connection = new SqlConnection(connectionName))
            {
                connection.Open();
                string sqlExpression = "EXEC TaskProcedureGetAllTasks";
                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            tasks.Add(CreateTaskVersion(reader));
                        }
                    }
                }
                connection.Close();
            }
            return tasks;
        }
        // НЕПРАВИЛЬНО, нужно получить все версии всех заданий, затем создать уникальные объекты заданий и вобрать в них коллекцию версий
        private Task CreateTaskVersion(SqlDataReader reader)
        {
            Task task = new Task
            {
                ID = Convert.ToInt32(reader["TaskID"]),
                Name = Convert.ToString(reader["TaskName"]),
                Description = Convert.ToString(reader["Description"]),
                Theme = new Theme() // ссылка на тему
                {
                    ID = Convert.ToInt32(reader["TaskID"]),
                    Name = Convert.ToString(reader["ThemeName"])
                },
                CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                ExpireDate = Convert.ToDateTime(reader["ExpireDate"]),
                Creator = new Employee()  // должна быть ссылка на работника
                {
                    ID = Convert.ToInt32(reader["CreatorID"]),
                    Name = Convert.ToString(reader["Creator"])
                }
            };
            task.TaskVersions.Add(new TaskVersion()
            {
                ID = Convert.ToInt32(reader["TaskVersionID"]),
                MoneyAward = Convert.ToDecimal(reader["MoneyAward"]), // null проверка
                PerformerID = Convert.ToInt32(reader["PerformerID"]),
                Performer = new Employee(),
                Version = Convert.ToByte(reader["Version"]),
                Status = Convert. // конвертирование в enum
            });
            return task;
        }
    }
}
