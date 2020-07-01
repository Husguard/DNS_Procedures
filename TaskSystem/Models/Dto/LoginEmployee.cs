using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TaskSystem.Models.Dto
{
    [DataContract]
    public class LoginEmployee
    {
        /// <summary>
        /// Имя работника
        /// </summary>
        [DataMember(Name = "employeeName")]
        public string Name { get; set; }

        /// <summary>
        /// Уникальный логин работника
        /// </summary>
        [Required(ErrorMessage = "Не указан логин")]
        [DataMember(Name = "login")]
        public string Login { get; set; }
    }
}
