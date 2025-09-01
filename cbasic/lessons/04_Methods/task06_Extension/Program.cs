using System;
using Extensions;

class Program
{
    static void Main()
    {
        Console.Write("Input number: ");
        var num = int.Parse(Console.ReadLine());

        num.EvenCheck(); // вызываем как метод расширения
        Console.WriteLine("(◕‿◕)");
    }
}
