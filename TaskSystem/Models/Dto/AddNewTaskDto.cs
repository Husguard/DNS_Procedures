﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TaskSystem.Models.Dto
{
    [DataContract]
    public class AddNewTaskDto
    {
        [DataMember(Name = "taskName")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "themeId")]
        public int ThemeId { get; set; }

        [DataMember(Name = "expireDate")]
        public DateTime ExpireDate { get; set; }
    }
}
