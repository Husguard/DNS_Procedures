/// Класс окна добавления задания
function AddTask() {
	this.container = document.getElementById("addTask");
	this.validation = document.getElementById("addTaskValidation");

	/// Указатель на форму добавления задания
	this.form = document.getElementById("addTaskContent");

	/// Метод переключения видимости окна добавления задания
    this.toggle = function () {
		Toggle(this.container);
	}

	/// Метод добавления нового задания из данных формы
	this.submit = async function (e) {
		const taskName = this.form.elements["taskName"].value;
		if (this.form.elements["themeId"] == null) return this.validation.innerHTML = "Выберите тему";
		const taskTheme = this.form.elements["themeId"].value;

		if (this.form.elements["taskExpireDateTime"].value == "") return this.validation.innerHTML = "Выберите дату окончания";
		const taskDescription = this.form.elements["taskDescription"].value;
		const taskExpireDateTime = this.form.elements["taskExpireDateTime"].value;
		const data = await TaskRepository.AddNewTask(taskName, taskDescription, taskTheme, taskExpireDateTime);
		switch (data.status) {
			case 0: {
				alert("Задание успешно добавлено");
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
    }
}
/// создание объекта окна
const addTask = new AddTask();
