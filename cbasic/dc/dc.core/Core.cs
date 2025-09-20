using System; //Подключение пространства имен System
using System.Collections.Generic; //Подключение ПИ для использования связного списка LinkedList<T>

namespace dc.core //Используем пространство имен, созданное командой dotnet new classlib -n dc.core
{
    public class Core //Создаем публичный класс
    {
        public static List<ToDoItem> Schedule { get; set; } = new List<ToDoItem>();
        public static string UserName = "";

        public void CoreMethod() //Создаем публичный метод, который будем использовать в точке входа dc/dc.app/Program.cs
        {
            ToDoUser? user = null;
            bool exit = false; //Назначение и инициализация переменной для выхода из цикла while
            Globals.Menu.AddLast("Начать - /start"); //Добавление элемента в конец связного списка
            Globals.Menu.AddLast("Помощь - /help");
            Globals.Menu.AddLast("Информация - /info");
            Globals.Menu.AddLast("Выход - /exit");

            while (!exit) //Цикл работает, пока exit не станет равен true
            {
                MsgUtils.ShowMenu(Globals.Menu, Globals.UserName); //Вызов метода из класса MsgUtils
                string? command = Console.ReadLine();

                switch (command) //Конструкция switch
                {
                    case "-s":
                    case "/start": //Один из кейсов конструкции switch
                        if (UserName == "")
                        {
                            while (true)
                            {
                                try
                                {
                                    Console.Write("Введи свое имя: ");
                                    user = new ToDoUser(Console.ReadLine());
                                    UserName = user.TelegramUserName;
                                    MsgUtils.MenuForRegisteredUser();
                                    break;
                                }
                                catch (ArgumentException)
                                {
                                    MsgUtils.Nothing(UserName);
                                }
                            }
                            break;
                        }
                        else
                        {
                            MsgUtils.ErrorCase(UserName); //Вызов метода
                            break;
                        }
                    case "/help":
                        MsgUtils.CommandChoose(command, Globals.UserName); //Вызов метода
                        MsgUtils.ShowHelp();
                        break;
                    case "/info":
                        MsgUtils.CommandChoose(command, Globals.UserName);
                        MsgUtils.ShowInfo();
                        break;
                    case "+":
                    case "/addtask":
                        if (user == null) { MsgUtils.ErrorCase(UserName); break; }

                        while (true)
                        {
                            try
                            {
                                Console.Write("Введи описание новой задачи: ");
                                var task = new ToDoItem(user, Console.ReadLine());
                                Schedule?.Add(task);
                                break;
                            }
                            catch (ArgumentException)
                            {
                                MsgUtils.Nothing(UserName);
                            }
                        }
                        break;
                    case "-sw":
                    case "/showactivetasks":
                        MsgUtils.ActiveTaskView(Schedule);
                        break;
                    case "-saw":
                    case "/showalltasks":
                        MsgUtils.AllTaskView(Schedule);
                        break;
                    case "-ct":
                    case "/completetask":
                        MsgUtils.CompleteTask(Schedule);
                        break;
                    case "-r":
                    case "/removetask":
                        MsgUtils.DelTask();
                        break;
                    case "xx":
                    case "/exit":
                        exit = true; //Выход из цикла while
                        break;
                    default:
                        if (Globals.UserName != "" && (command == "/echo" || command == "/echo ")) //Проверка имени, чтобы команда не сработала до регистрации пользователя. Проверка на наличие команды.
                        {
                            Console.WriteLine($"\nДорогой {MsgUtils.UserName(Globals.UserName)}, для работы команды '/echo' после нее необходимо ввести аргумент (например, /echo Hello).");
                            break;
                        }
                        else if (Globals.UserName != "" && command?.Length > 6 && command.Substring(0, 6) == "/echo ") //Проверка имени, чтобы команда не сработала до регистрации пользователя. Проверка на наличие правильно введенной команды.
                        {
                            MsgUtils.CommandChoose("/echo", Globals.UserName);
                            Console.WriteLine(command.Substring(6));
                            break;
                        }
                        else
                        {
                            MsgUtils.ErrorCase(Globals.UserName);
                            break;
                        }
                }
            }
        }
    }
}
