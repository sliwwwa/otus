using System;
using dc.core; //Подключаем namespace бизнес-логики (Core)

namespace dc.app //Используем namespace, созданный командой dotnet new console -n dc.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var core = new Core(); //Создаем экземпляр класса (объект)
            core.CoreMethod(); //Вызываем метод для объекта
        }
    }
}
