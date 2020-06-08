using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class ThemeRepository : IThemeRepository
    {
        public List<Theme> Themes { get; set; }
        public void AddTheme(string name)
        {
            Themes.Add(new Theme() { Name = name }); // нужно исправить способ получения ID
        }
        public IEnumerable<Theme> GetAllThemes()
        {
            return Themes;
        }
        public IEnumerable<Theme> GetThemesByName(string name)
        {
            return Themes.Where((theme) => theme.Name.StartsWith(name));
        }
       
    }
}
