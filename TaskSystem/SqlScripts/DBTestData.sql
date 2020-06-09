INSERT INTO TaskStatus(Name) VALUES('Новое');
INSERT INTO TaskStatus(Name) VALUES('В работе');
INSERT INTO TaskStatus(Name) VALUES('Приостановлено');
INSERT INTO TaskStatus(Name) VALUES('Выполнено');
INSERT INTO TaskStatus(Name) VALUES('Отменено');

INSERT INTO TaskEmployee(Name, Login) VALUES('ФИО1', 'login1');
INSERT INTO TaskEmployee(Name, Login) VALUES('ФИО2', 'лоигн1');
INSERT INTO TaskEmployee(Name, Login) VALUES('FIO1', 'losd');
INSERT INTO TaskEmployee(Name, Login) VALUES('FIO3', 'testlogin');

INSERT INTO TaskTheme(Name) VALUES('Theme1');
INSERT INTO TaskTheme(Name) VALUES('Тема2');

EXEC TaskProcedureAddTask 'taskName', 'taskDescription', 1, 1, '20210101'
EXEC TaskProcedureAddTask 'taskName1', 'taskDescription1', 2, 1, '20210101'
EXEC TaskProcedureAddTask 'taskName2', 'taskDescription2', 1, 2, '20210101'
EXEC TaskProcedureAddTask 'taskName3', 'taskDescription3', 1, 2, '20210101'