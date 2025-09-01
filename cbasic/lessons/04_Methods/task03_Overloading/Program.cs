using System;

class Program
{
    static void Main()
    {
        string name = "slava";

        Greet();
        Greet(name);
    }

    public static void Greet()
    {
        Console.WriteLine("Greetings, user!");
    }

    public static void Greet(string name)
    {
        Console.WriteLine($"Greetings, {name}!");
    }
}
