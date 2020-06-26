/// Класс окна добавления задания
function AddTask() {
	this.container = "addTask"; /// указатель на окно добавления задания

	/// Указатель на форму добавления задания
	this.form = document.getElementById("addTaskContent");

	/// Метод переключения видимости окна добавления задания
    this.toggle = function () {
		Toggle(this.container);
	}

	/// Метод добавления нового задания из данных формы
	this.submit = async function (e) {
		const taskName = this.form.elements["taskName"].value;
		const taskTheme = this.form.elements["taskTheme"].value;
		const taskDescription = this.form.elements["taskDescription"].value;
		const taskExpireDateTime = this.form.elements["taskExpireDateTime"].value;
		const data = await TaskRepository.AddNewTask(taskName, taskDescription, taskTheme, taskExpireDateTime);
		switch (data.status) {
			case 0: {
				alert("Задание успешно добавлено");
				this.toggle();
			}
			case 1: {
				alert(data.errorMessage);
			}
			case 2: {
				alert("Сервер не доступен");
			}
		}
    }
}
/// создание объекта окна
const addTask = new AddTask();
