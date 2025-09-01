namespace Extensions
{
public static class IntExtensions
{
    public static void EvenCheck(this int a)
    {
        if (a % 2 == 0)
        {
            Console.WriteLine("Четное");
        }
        else
        {
            Console.WriteLine("Нечетное");
        }
    }
}
}
