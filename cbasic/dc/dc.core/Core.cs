using System; //Подключение пространства имен System
using System.Collections.Generic; //Подключение ПИ для использования связного списка LinkedList<T>

namespace dc.core //Используем пространство имен, созданное командой dotnet new classlib -n dc.core
{
    public class Core //Создаем публичный класс
    {
        public void CoreMethod() //Создаем публичный метод, который будем использовать в точке входа dc/dc.app/Program.cs
        {
            bool exit = false; //Назначение и инициализация переменной для выхода из цикла while
            string? name = ""; //Назначение и инициализация переменной
            var schedule = new List<string>();
            LinkedList<string> menu = new(); //Используем связный список LinkedList<T> из пространства имен System.Collections.Generic
            menu.AddLast("Начать - /start"); //Добавление элемента в конец связного списка
            menu.AddLast("Помощь - /help");
            menu.AddLast("Информация - /info");
            menu.AddLast("Добавить задачу - /addtask");
            menu.AddLast("Список задач - /showtasks");
            menu.AddLast("Удалить задачу - /removetask");
            menu.AddLast("Выход - /exit");

            while (!exit) //Цикл работает, пока exit не станет равен true
            {
                MsgUtils.ShowMenu(menu, name); //Вызов метода из класса MsgUtils
                string? command = Console.ReadLine();

                switch (command) //Конструкция switch
                {
                    case "-s":
                    case "/start": //Один из кейсов конструкции switch
                        if (name == "") //Проверка на то, ввел ли пользователь имя
                        {
                            Console.Write("Введи своё имя: ");
                            name = Console.ReadLine();
                            if (name != "") //Проверка на то, ввел ли пользователь имя
                            {
                                menu.RemoveFirst(); //Удаление первого элемента связного списка
                                var node = menu.Find("Выход - /exit"); //Поиск элемента связного списка с данным содержимым
                                if (node != null) //Если нашли, то
                                {
                                    menu.AddBefore(node, "Вывод строки - /echo"); //Добавление элемента связного списка с данным содержимым перед найденным ранее
                                }
                            }
                            break;
                        }
                        else
                        {
                            MsgUtils.ErrorCase(name); //Вызов метода
                            break;
                        }
                    case "/help":
                        MsgUtils.CommandChoose(command, name); //Вызов метода
                        Console.WriteLine("---Справочник для помощи в навигации по программе---");
                        Console.WriteLine("Команда '/start' - начало работы с приложением, регистрация пользователя");
                        Console.WriteLine("Команда '/help' - справка");
                        Console.WriteLine("Команда '/info' - версия приложения, дата выпуска");
                        Console.WriteLine("Команда '/echo' - при вводе этой команды с аргументом (например, /echo Hello), программа возвращает введенный текст (в данном примере 'Hello')");
                        Console.WriteLine("Команда '/exit' - выход из программы (совет: нажми клавишу 'Enter' второй раз после ввода команды для более быстрого выхода;))\n");
                        Console.WriteLine("Команда '/addtask' - добавление задачи и ее описание в конец списка");
                        Console.WriteLine("Команда '/showtasks' - отображение списка всех задач");
                        Console.WriteLine("Команда '/removetask' - удаление задачи, для удаления введи ее название (вместе с символом '/')");
                        break;
                    case "/info":
                        MsgUtils.CommandChoose(command, name);
                        Console.WriteLine("v1.0.1");
                        Console.WriteLine("Release date: 20.08.2025\n");
                        break;
                    case "+":
                    case "/addtask":
                        Console.Write("\nВведи описание новой задачи: ");
                        string task = Console.ReadLine();
                        MsgUtils.AddTask(schedule, task); // Вызов метода
                        Console.Write($"\nЗадача '{task}' успешно добавлена\n");
                        break;
                    case "-sw":
                    case "/showtasks":
                        MsgUtils.ScheduleView(schedule); //Вызов метода
                        break;
                    case "-r":
                    case "/removetask":
                        if (schedule?.Count != 0) //Проверка на наличие элементов в списке
                        {
                            Console.WriteLine("\nСписок твоих задач:");
                            MsgUtils.ScheduleView(schedule); //Вызов метода
                            Console.Write("\nВведи номер задачи для удаления: ");
                            int taskNum = Convert.ToInt32(Console.ReadLine());
                            if (taskNum > schedule?.Count || taskNum <= 0) //Проверка на валидность введенного значения
                            {
                                Console.WriteLine("\nТакого номера задачи не существует, введи корректный номер задачи.");
                            }
                            else
                            {
                                MsgUtils.DelTask(schedule, taskNum); //Вызов метода
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n*** У тебя пока что нет задач ***");
                        }
                        break;
                    case "/exit":
                        exit = true; //Выход из цикла while
                        break;
                    default:
                        if (name != "" && (command == "/echo" || command == "/echo ")) //Проверка имени, чтобы команда не сработала до регистрации пользователя. Проверка на наличие команды.
                        {
                            Console.WriteLine($"\nДорогой {MsgUtils.UserName(name)}, для работы команды '/echo' после нее необходимо ввести аргумент (например, /echo Hello).");
                            break;
                        }
                        else if (name != "" && command?.Length > 6 && command.Substring(0, 6) == "/echo ") //Проверка имени, чтобы команда не сработала до регистрации пользователя. Проверка на наличие правильно введенной команды.
                        {
                            MsgUtils.CommandChoose("/echo", name);
                            Console.WriteLine(command.Substring(6));
                            break;
                        }
                        else
                        {
                            MsgUtils.ErrorCase(name);
                            break;
                        }
                }
            }
        }
    }
}
