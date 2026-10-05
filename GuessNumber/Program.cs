using System;

namespace GuessGame
{
    class Game
    {
        static void Main(string[] args)
        {
            Console.WriteLine("这是一个猜数字的游戏，范围1到100的整数");
            Random number = new Random();
            int num = number.Next(1, 101);
            int min = 1;
            int max = 100;
            int n = 0;
            Console.Write($"请在{min}到{max}中输入一个整数：");
            string input = Console.ReadLine();
            int tag = int.Parse(input);
            n++;
            while (tag != num)
            {
                if (tag < num)
                {
                    min = tag;
                    n++;
                    Console.WriteLine($"小了，请在{min}到{max}中输入一个整数：");
                    input = Console.ReadLine();
                    tag = int.Parse(input);
                }
                else
                {
                    max = tag;
                    n++;
                    Console.WriteLine($"大了，请在{min}到{max}中输入一个整数：");
                    input = Console.ReadLine();
                    tag = int.Parse(input);
                }
            }
            Console.WriteLine($"猜对了，总共使用了{n}次");
        }
    }
}