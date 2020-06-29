/// Инициализация репозитория тем
const ThemeRepository = {
    /// Метод получения тем, у которых название начинается с введенного значения
    /// <name> - название темы
    GetThemesByName: function (name) {
        return FetchGet("GetThemesByName", {
            name: name
        });
    },

    /// Метод добавления новой темы
    /// <name> - название новой темы
    AddTheme: function (name) {
        return FetchPost("AddTheme", {
            themeName: name
        });
    },

    /// Метод создания окна выбора желаемой темы, полученных из метода GetThemesByName
    /// <name> - название желаемой темы
    ShowSuggests: async function (name) {
        const suggests = document.getElementById("suggestsThemes");
        suggests.style.display = 'none';
        while (suggests.firstChild) {
            suggests.removeChild(suggests.firstChild);
        }
        if (name.length > 0) {
            const json = await ThemeRepository.GetThemesByName(name);
            const input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", "themeId");
            for (let i = 0; i < json.result.length; i++) {
                const option = document.createElement('a');
                option.innerText = json.result[i].themeName;
                option.addEventListener('click', function () {
                    document.getElementById("taskTheme").value = this.innerText;
                    input.setAttribute("value", json.result[i].themeId); // выбор значения обязателен, нет проверки выбрана ли тема
                    suggests.style.display = 'none';
                });
                suggests.append(option);
            }
            suggests.append(input); // в родительский div бы
            suggests.style.display = 'block';
        }
    }
}
