using System;

namespace GuessGame
{
    class Game
    {
        static void Main(string[] args)
        {
            Console.WriteLine("这是一个猜数字的游戏，范围1到100的整数");
            Random random = new Random();
            int num = random.Next(1, 101);
            int min = 1;
            int max = 100;
            int count = 0;
            string input;
            int guess = 0;
            bool play = true;
            int best = int.MaxValue;
            while (play)
            {
                while (guess != num)
                {
                    Console.Write($"请在{min}到{max}中输入一个整数：");
                    input = Console.ReadLine();
                    if (input == null) 
                    { 
                        play = false; 
                        break;
                    }
                    if (!int.TryParse(input, out guess))
                    {
                        Console.WriteLine("非法内容");
                        continue;
                    }
                    count++;
                    if (guess < num)
                    {
                        min = guess;
                        Console.Write($"小了，");
                    }
                    else if (guess > num)
                    {
                        max = guess;
                        Console.Write($"大了，");
                    }
                }
                if (!play) 
                    break;
                Console.WriteLine($"猜对了，总共使用了{count}次");
                if (best > count)
                {
                    best = count;
                }
                Console.WriteLine("输入任何内容再来一局，输入1结束游戏");
                input = Console.ReadLine();
                if (input == null)
                { 
                    play = false;
                    break; 
                }
                if (input == "1")
                    play = false;
                if (input != null)
                {
                    guess = 0;
                    num = random.Next(1, 101);
                    count = 0;
                    min = 1;
                    max = 100;
                }
            }
            Console.WriteLine($"最佳次数是{best}次");
        }
    }
}