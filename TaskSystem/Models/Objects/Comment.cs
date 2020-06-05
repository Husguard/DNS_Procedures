using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    public class Comment
    {
        public Task Task { get; set; }
        public Employee Employee { get; set; }
        public string Message { get; set; }
    }
}
