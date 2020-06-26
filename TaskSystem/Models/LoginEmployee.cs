using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models
{
    public class LoginEmployee
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }
    }
}
