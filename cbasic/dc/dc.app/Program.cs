using System;
using dc.core; //Подключаем namespace бизнес-логики (Core)
using Otus.ToDoList.ConsoleBot;
using Otus.ToDoList.ConsoleBot.Types;

namespace dc.app //Используем namespace, созданный командой dotnet new console -n dc.app
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
/*            var core = new Core(); //Создаем экземпляр класса (объект)
            core.CoreMethod(); //Вызываем метод для объекта*/
                IUserService userService = new UserService();

                var botClient = new ConsoleBotClient();
                var toDoService = new ToDoService();
                var handler = new UpdateHandler(userService, toDoService);
//                ToDoUser? user = null;
                Globals.Menu.AddLast("Начать - /start"); //Добавление элемента в конец связного списка
                Globals.Menu.AddLast("Помощь - /help");
                Globals.Menu.AddLast("Информация - /info");
                Globals.Menu.AddLast("Выход - /exit");

                MsgUtils.ShowMenu(Globals.Menu, Globals.UserName); //Вызов метода из класса MsgUtils
                botClient.StartReceiving(handler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
