using System;
using System.Xml.Linq;

namespace ScoreManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string userinput;
            string[] parts;
            int score;
            Dictionary<string, int> scor = new Dictionary<string, int>();
            var scores = new Dictionary<string, int>();
            if (File.Exists("score.txt")) 
            {
                foreach (string line in File.ReadAllLines("score.txt"))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var part = line.Split(',');
                    scores[part[0]] = int.Parse(part[1]);
                }
            }
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
                string input = Console.ReadLine() ?? "";
                if (input == null || input == "0")
                {
                    foreach (var n in scores.Keys)
                    {
                        File.WriteAllLines("score.txt", scores.Select(kv => $"{kv.Key},{kv.Value}"));
                    }
                    break;
                }
                switch (input)
                {
                    case "1":
                        Console.Write("请添加学生的姓名和成绩：");
                        userinput = Console.ReadLine() ?? "";
                        if (userinput == null)
                            break;
                        parts = userinput.Split(',');
                        if (parts.Length != 2 || !int.TryParse(parts[1], out score))
                        {
                            Console.WriteLine("非法内容格式");
                            break;
                        }
                        if (!scores.ContainsKey(parts[0]))
                        {
                            Console.WriteLine("添加成功");
                        }
                        else
                        {
                            Console.WriteLine("该学生已存在");
                            break;
                        }
                        scores[parts[0]] = score;
                        break;
                    case "2":
                        Console.Write("请输入要删除的学生姓名：");
                        userinput = Console.ReadLine() ?? "";
                        if (userinput == null)
                            break;
                        if (scores.TryGetValue(userinput, out score))
                        {
                            scores.Remove(userinput);
                            Console.WriteLine("已成功删除");
                        }
                        else
                        {
                            Console.WriteLine("该学生未存在");
                        }
                        break;
                    case "3":
                        Console.Write("请输入要修改成绩的学生姓名和成绩：");
                        userinput = Console.ReadLine() ?? "";
                        if (userinput == null)
                            break;
                        parts = userinput.Split(',');
                        if (parts.Length != 2 || !int.TryParse(parts[1], out score))
                        {
                            Console.WriteLine("非法内容格式");
                            break;
                        }
                        if(!scores.ContainsKey(parts[0]))
                        {
                            Console.WriteLine("该学生不存在");
                            break;
                        }
                        Console.WriteLine("修改成功");
                        scores[parts[0]] = score;
                        break;
                    case "4":
                        Console.Write("请选择要查询的学生的姓名：");
                        userinput = Console.ReadLine() ?? "";
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
                        var byscore = scores.OrderByDescending(s => s.Value);
                        foreach(var s in byscore)
                        {
                            Console.WriteLine($"{s.Key}，{s.Value}");
                        }
                        break;
                    case "6":
                        double avg = scores.Count > 0 ? scores.Average(av => av.Value) : 0;
                        int max = scores.Count > 0 ? scores.Max(av => av.Value) : 0;
                        int min = scores.Count > 0 ? scores.Min(av => av.Value) : 0;
                        Console.WriteLine($"平均值是{avg}，最大值是{max}，最小值是{min}");
                        break;
                    default:
                        Console.WriteLine("非法内容格式");
                        break;

                }
            }
        }
    }
}