using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class TaskVersion
    {
        
        public int ID { get; set; }
        public byte Version { get; set; }
        public Status Status { get; set; }
        public int? PerformerID { get; set; }
        public Employee Performer { get; set; }
        public decimal? MoneyAward { get; set; }
    }
}
