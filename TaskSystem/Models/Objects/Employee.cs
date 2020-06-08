using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public IEnumerable<Task> CreatedTasks { get; set; }
        public IEnumerable<TaskVersion> AcceptedTasks { get; set; }
    }
}
