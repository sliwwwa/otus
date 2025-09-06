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
            Console.WriteLine($"\nДорогой {UserName(name)}, ты ошибся при вводе команды, попробуй ещё раз");
        }

        public static void ScheduleView(List<string> schedule)
        {
            int count = 1;

            Console.WriteLine("");
            if (schedule?.Count != 0)
            {
                foreach (var task in schedule)
                {
                    Console.WriteLine($"{count}. {task}");
                    count++;
                }
            }
            else
            {
                Console.WriteLine("*** У тебя пока что нет задач ***");
            }
        }

        public static void AddTask(List<string> schedule, string task)
        {
            schedule.Add(task);
        }

        public static void DelTask(List<string> schedule, int taskNum)
        {
            schedule.RemoveAt(taskNum - 1);
        }
    }
}
