using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITheme
    {
        public void AddTheme(string name);
        public List<Theme> GetAllThemes();
        public List<Theme> GetThemesByName(string name);
    }
}
