/// Класс окна добавления задания
function AddTask() {
	this.container = "addTask"; /// указатель на окно добавления задания
	this.validation = "addTaskValidation";

	/// Указатель на форму добавления задания
	this.form = document.getElementById("addTaskContent");

	/// Метод переключения видимости окна добавления задания
    this.toggle = function () {
		Toggle(this.container);
	}

	/// Метод добавления нового задания из данных формы
	this.submit = async function (e) {
		const taskName = this.form.elements["taskName"].value;
		if (this.form.elements["themeId"] == null) return Insert("Выберите тему", this.validation);
		const taskTheme = this.form.elements["themeId"].value;

		if (this.form.elements["taskExpireDateTime"].value == "") return Insert("Выберите дату окончания", this.validation);
		const taskDescription = this.form.elements["taskDescription"].value;
		const taskExpireDateTime = this.form.elements["taskExpireDateTime"].value;
		const data = await TaskRepository.AddNewTask(taskName, taskDescription, taskTheme, taskExpireDateTime);
		switch (data.status) {
			case 0: {
				alert("Задание успешно добавлено");
				this.toggle();
				break;
			}
			case 1: {
				Insert(data.errorMessage, this.validation);
				break;
			}
			case 2: {
				alert("Сервер недоступен");
				break;
			}
		}
    }
}
/// создание объекта окна
const addTask = new AddTask();
