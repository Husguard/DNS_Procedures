using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class WorkTask
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ThemeID { get; set; }
        public int CreatorID { get; set; }
        public byte Version { get; set; }
        public Status Status { get; set; }
        public int? PerformerID { get; set; }
        public decimal? MoneyAward { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public WorkTask()
        {

        }
    }
}
