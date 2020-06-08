using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    public enum Status
    {
        New = 1,
        InWork,
        Paused,
        Completed,
        Canceled
    }
}
