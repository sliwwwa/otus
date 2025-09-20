using System;

namespace dc.core
{
    public static class MsgUtils
    {
        public static string UserName(string? name) //Метод определяет и возвращает имя в требуемое место
        {
            string? showName; //Объявление переменной

            if (name == "") //Проверка на наличие имени
            {
                showName = "гость"; //Инициализация переменной
            }
            else
            {
                showName = name;
            }
            return showName; //Возврат значения
        }

        public static void MenuForRegisteredUser()
        {
            Globals.Menu.RemoveFirst(); //Удаление первого элемента связного списка
            var node = Globals.Menu.Find("Выход - /exit"); //Поиск элемента связного списка с данным содержимым

            if (node != null) //Если нашли, то
            {
                string[] items = {
                    "Добавить задачу - /addtask",
                    "Список активных задач - /showactivetasks",
                    "Список всех задач - /showalltasks",
                    "Завершить задачу - /completetask",
                    "Удалить задачу - /removetask",
                    "Вывод строки - /echo"
                };
                foreach (string item in items)
                {
                    Globals.Menu.AddBefore(node, item);
                }
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("---Справочник для помощи в навигации по программе---");
            Console.WriteLine("Команда '/start' - начало работы с приложением, регистрация пользователя");
            Console.WriteLine("Команда '/help' - справка");
            Console.WriteLine("Команда '/info' - версия приложения, дата выпуска");
            Console.WriteLine("Команда '/echo' - при вводе этой команды с аргументом (например, /echo Hello), программа возвращает введенный текст (в данном примере 'Hello')");
            Console.WriteLine("Команда '/exit' - выход из программы (совет: нажми клавишу 'Enter' второй раз после ввода команды для более быстрого выхода;))");
            Console.WriteLine("Команда '/addtask' - добавление задачи и ее описание в конец списка");
            Console.WriteLine("Команда '/showactivetasks' - отображение списка активных задач");
            Console.WriteLine("Команда '/showalltasks' - отображение списка всех задач");
            Console.WriteLine("Команда '/completetask' - выполнение активной задачи, найденной по Id");
            Console.WriteLine("Команда '/removetask' - удаление задачи, для удаления введи ее название (вместе с символом '/')");
        }

        public static void ShowInfo()
        {
            Console.WriteLine(Globals.Version);
            Console.WriteLine($"{Globals.ReleaseDate}\n");

        }

        public static void AddTask(List<string> schedule, string task) //Метод добавляения элементов в список schedule
        {
            schedule.Add(task); //Добавление элемента в конец списка schedule
        }

        public static void CompleteTask(List<ToDoItem> schedule)
        {
            int count = 0;
            string taskId = "";
            Console.Write("\nВведи id задачи для выполнения: ");
            taskId = Console.ReadLine();

            foreach (ToDoItem task in schedule)
            {
                if (taskId == task.Id.ToString())
                {
                    task.State = ToDoItem.ToDoItemState.Completed;
                    task.StateChangedAt = DateTime.UtcNow;
                    Console.WriteLine("\nЗадача выполнена.\n");
                    count++;
                }
            }

            if (count == 0)
                {
                    Console.WriteLine("Задача с таким Id не найдена");
                }
            
            MsgUtils.AllTaskView(schedule);
        }

        public static void AllTaskView(List<ToDoItem> schedule) //Метод вывода на экран элементов списка schedule
        {
            int count = 1;

            Console.WriteLine("");
            if (schedule?.Count != 0) //Проверка на наличие элементов в списке
            {
                foreach (var task in schedule) //Перечисление элементов в списке
                {
                    if (task.State == ToDoItem.ToDoItemState.Active || task.State == ToDoItem.ToDoItemState.Completed)
                    {
                        Console.WriteLine($"{count}. ({task.State}) {task.Name} - {task.CreatedAt} - {task.Id} - completed at: {task.StateChangedAt}");
                        count++; //Инкремент для вывода номера задачи в списке
                    }
                }
            }
            else
            {
                Console.WriteLine("У тебя пока что нет задач.");
            }
        }

        public static void ActiveTaskView(List<ToDoItem> schedule)
        {
            int count = 1;

            Console.WriteLine("");
            if (schedule?.Count != 0) //Проверка на наличие элементов в списке
            {
                foreach (var task in schedule) //Перечисление элементов в списке
                {
                    if (task.State == ToDoItem.ToDoItemState.Active)
                    {
                        Console.WriteLine($"{count}. ({task.State}) {task.Name} - {task.CreatedAt} - {task.Id}");
                        count++; //Инкремент для вывода номера задачи в списке
                    }
                }
            }
            else
            {
                Console.WriteLine("У тебя пока что нет выполненных задач.");
            }
        }

        public static void DelTask() //Метод удаления элемента из списка schedule
        {
            if (Core.Schedule?.Count != 0) //Проверка на наличие элементов в списке
            {
                Console.WriteLine("\nСписок твоих задач:");
                MsgUtils.AllTaskView(Core.Schedule); //Вызов метода
                Console.Write("\nВведи номер задачи для удаления: ");
                int taskNum = Convert.ToInt32(Console.ReadLine());
                if (taskNum > Core.Schedule?.Count || taskNum <= 0) //Проверка на валидность введенного значения
                {
                    Console.WriteLine("\nТакого номера задачи не существует, введи корректный номер задачи.");
                }
                else
                {
                    Core.Schedule.RemoveAt(taskNum - 1); //Удаление элемента с указанным индексом из списка                    
                    Console.WriteLine("Ты успешно удалил задачу.");
                    MsgUtils.AllTaskView(Core.Schedule); //Вызов метода
                }
            }
            else
            {
                Console.WriteLine("\nУ тебя пока что нет задач.");
            }
        }

        public static void ShowMenu(LinkedList<string> menu, string? name) //Метод выводит меню из связного списка
        {
            Console.WriteLine($"\nПриветствую тебя, {UserName(name)}!");
            Console.WriteLine("Бот понимает следующие команды:\n");
            foreach (string item in menu) //Вывод в цикле, ограниченном элементами списка
            {
                Console.WriteLine(item);
            }
            Console.Write("\nВведи команду: ");
        }

        public static void CommandChoose(string? command, string? name) //Метод вежливо приветствует пользователя и сообщает о том, какую команду он ввел
        {
            Console.WriteLine($"\nДорогой {UserName(name)}, ты выбрал команду {command}, наслаждайся!\n");
        }

        public static void ErrorCase(string? name) //Метод обработки невалидных (неизвестных приложению) команд
        {
            Console.WriteLine($"\nДорогой {UserName(name)}, ты ошибся при вводе команды, попробуй ещё раз.");
        }

        public static void Nothing(string? name)
        {
            Console.WriteLine($"\nДорогой {UserName(name)}, ты ничего не ввел, попробуй ещё раз.");
        }

        public static void NamePrint()
        {
            Console.WriteLine(Core.UserName);
        }
    }
}