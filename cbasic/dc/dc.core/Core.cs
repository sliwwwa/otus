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
            LinkedList<string> menu = new(); //Используем связный список LinkedList<T> из пространства имен System.Collections.Generic
            menu.AddLast("Начать - /start"); //Добавление элемента в конец связного списка
            menu.AddLast("Помощь - /help");
            menu.AddLast("Информация - /info");
            menu.AddLast("Выход - /exit");

            while (!exit) //Цикл работает, пока exit не станет равен true
            {
                MsgUtils.ShowMenu(menu, name); //Вызов метода из класса MsgUtils
                string? command = Console.ReadLine();

                switch (command) //Конструкция switch
                {
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
                        break;
                    case "/info":
                        MsgUtils.CommandChoose(command, name);
                        Console.WriteLine("v1.0.0");
                        Console.WriteLine("Release date: 20.08.2025\n");
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
