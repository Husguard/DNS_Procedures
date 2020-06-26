// Класс окна добавления новой темы
function AddTheme() {
	this.container = "addTheme"; /// указатель на окно добавление темы

	/// Указатель на форму добавления новой темы
	this.form = document.getElementById("addThemeContent");

	/// Метод переключения видимости окна добавления новой темы
    this.toggle = function () {
		Toggle(this.container);
	};

	/// Метод добавления новой темы из данных формы
    this.submit = async function () {
        const name = this.form.elements["themeName"].value;
        const data = await ThemeRepository.AddTheme(name);
		switch (data.status) {
			case 0: {
				alert("Тема успешно добавлена");
				this.toggle();
				break;
			}
			case 1: {
				alert(data.errorMessage);
				break;
			}
			case 2: {
				alert("Сервер не доступен");
				break;
			}
		}
    };
}
/// создание объекта добавления темы
const addTheme = new AddTheme();