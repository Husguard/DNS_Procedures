// Класс окна добавления новой темы
function AddTheme() {
	this.container = document.getElementById("addTheme"); /// указатель на окно добавление темы
	this.validation = document.getElementById("addThemeValidation");

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
				this.form.reset();
				break;
			}
			case 1: {
				this.validation.innerHTML = data.errorMessage;
				break;
			}
			case 2: {
				alert("Возникла критическая ошибка");
				break;
			}
		}
    };
}
/// создание объекта добавления темы
const addTheme = new AddTheme();