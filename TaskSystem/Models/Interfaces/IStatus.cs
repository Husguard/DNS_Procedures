using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface IStatus
    {
        public void UpdateStatusOfTask(Task task, Status status);
    }
}
