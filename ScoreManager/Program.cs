using System;

namespace ScoreManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string userinput;
            string[] parts;
            int score;
            string name;
            Dictionary<string, int> scores = new Dictionary<string, int>();
            while (true)
            {
                Console.WriteLine(@"===== 成绩管理器 =====
1. 添加学生成绩
2. 删除学生
3. 修改学生成绩
4. 查询学生成绩
5. 显示所有学生（按成绩从高到低）
6. 显示统计信息（平均分/最高分/最低分）
0. 退出程序
");
                string input = Console.ReadLine();
                if (input == null || input == "0")
                {
                    break;
                }
                switch (input)
                {
                    case "1":
                        Console.Write("请添加学生的姓名和成绩：");
                        userinput = Console.ReadLine();
                        if (userinput == null)
                            break;
                        parts = userinput.Split(',');
                        if (parts.Length != 2 || !int.TryParse(parts[1], out score))
                        {
                            Console.WriteLine("非法内容格式");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("添加成功");
                        }
                        scores[parts[0]] = score;
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        Console.Write("请选择要查询的学生的姓名：");
                        userinput = Console.ReadLine();
                        if (userinput == null)
                            break;
                        if (scores.TryGetValue(userinput, out score))
                        {
                            Console.WriteLine($"{userinput}，{score}");
                        }
                        else
                        {
                            Console.WriteLine("未能查询得到");
                        }
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    default:
                        Console.WriteLine("非法内容格式");
                        break;

                }
            }
        }
    }
}