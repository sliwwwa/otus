using System;

public static class Program
{
    static void Main()
    {
        int[] nums = { 1, 2, 3, 4, 5 };

        Multiplier(nums);
    }

    static void Multiplier(int[] nums)
    {
        int result = 1;

        foreach(int i in nums)
        {
            result = result * i;
        }
        Console.WriteLine($"The result is {result}");
    }
}
