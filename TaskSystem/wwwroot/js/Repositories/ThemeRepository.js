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
    ShowSuggests: function (name) {
        const suggests = document.getElementById("suggestsThemes")
        suggests.style.display = 'none';
        while (suggests.firstChild) {
            suggests.removeChild(suggests.firstChild);
        }
        if (name > 0) {
            ThemeRepository.GetThemesByName(name).then(
                (json) => {
                    for (let i = 0; i < json.length; i++) {
                        let btnShow = document.createElement('a');
                        btnShow.innerText = json[i];
                        btnShow.addEventListener('click', function () {
                            document.getElementById("taskTheme").innerHTML = this.innerText;
                            suggests.style.display = 'none';
                        });
                        suggests.append(btnShow);
                    }
                    suggests.style.display = 'block';
                }
            );
        }
    }
}
