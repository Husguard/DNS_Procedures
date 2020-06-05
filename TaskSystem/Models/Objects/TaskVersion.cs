﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    public class TaskVersion
    {
        public int ID { get; set; }
        public Task Task { get; set; }
        public byte Version { get; set; }
        public Status Status { get; set; }
        public Employee Performer { get; set; }
        public decimal MoneyAward { get; set; }
    }
}
