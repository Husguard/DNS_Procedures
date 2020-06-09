using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    public class Comment
    {
        public int TaskID { get; set; }
        public int EmployeeID { get; set; }
        public string Message { get; set; }
    }
}
