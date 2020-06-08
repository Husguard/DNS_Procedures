using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface IThemeRepository
    {
        public void AddTheme(string name);
        public IEnumerable<Theme> GetAllThemes();
        public IEnumerable<Theme> GetThemesByName(string name);
    }
}
