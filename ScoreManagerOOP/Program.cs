using System;

namespace ScoreManager
{
    class Student
    {
        public void PrintInfo()
        {
            Console.WriteLine($"{Name}，{Score}");
        }
        public string Name
        {
            get; set;
        }
        public int Score
        {
            get; set;
        }
    }
    class ScoreManager
    {
        private List<Student> students = new List<Student>();
        public void Add(string name, int score)
        {
            if (students.Any(s => s.Name == name))
            {
                Console.WriteLine("已存在该学生，添加失败！");
            }
            else
            {
                Student student = new Student();
                student.Name = name;
                student.Score = score;
                students.Add(student);
                Console.WriteLine("已成功添加！");
            }
        }
        public void Remove(string name)
        {
            if (students.Any(s => s.Name == name))
            {
                students.Remove(students.Find(s => s.Name == name));
                Console.WriteLine("已成功删除！");
            }
            else
            {
                Console.WriteLine("未存在该学生，删除失败！");
            }
        }
        public void Update(string name, int newScore)
        {
            if (students.Any(s => s.Name == name))
            {
                students.Find(s => s.Name == name).Score = newScore;
                Console.WriteLine("已成功修改！");
            }
            else
            {
                Console.WriteLine("未存在该学生，修改失败！");
            }
        }
        public void Find(string name)
        {
            Student sdt = students.Find(s => s.Name == name);
            if (sdt != null)
            {
                sdt.PrintInfo();
            }
            else
            {
                Console.WriteLine("未存在该学生！");
            }
        }
        public void PrintAll()
        {
            foreach (var stu in students)
            {
                stu.PrintInfo();
            }
        }
        public void PrintAllScore()
        {
            var scoreAlls = students.OrderByDescending(s => s.Score);
            foreach (var scoreAll in scoreAlls)
            {
                scoreAll.PrintInfo();
            }
        }
        public void PrintStats()
        {
            if(students.Count > 0)
            {
                double avg = students.Average(s => s.Score);
                double max = students.Max(s => s.Score);
                double min = students.Min(s => s.Score);
                Console.WriteLine($"平均值是{avg}，最大值是{max}，最小值是{min}");
            }
            else
            {
                Console.WriteLine("没有任何学生成绩！");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            string[] parts;
            ScoreManager manager = new ScoreManager();
            while (true)
            {
                Console.WriteLine(@"===== 成绩管理器 =====
1. 添加学生成绩
2. 删除学生
3. 修改学生成绩
4. 查询学生成绩
5. 显示全部
6. 显示所有学生（按成绩从高到低）
7. 显示统计信息（平均分/最高分/最低分）
0. 退出程序
");
                input = Console.ReadLine() ?? "";
                if (input == null || input == "0")
                {
                    Console.WriteLine("已退出程序！");
                    break;
                }
                switch (input)
                {
                    case "1":
                        Console.Write("请输入添加的学生姓名和成绩：");
                        input = Console.ReadLine() ?? "";
                        if (input == null)
                        {
                            break;
                        }
                        parts = input.Split(',');
                        if (parts.Length != 2 || !int.TryParse(parts[1], out int score1))
                        {
                            Console.WriteLine("非法输入！");
                        }
                        else
                        {
                            manager.Add(parts[0], score1);
                        }
                        break;
                    case "2":
                        Console.Write("请输入删除的学生姓名：");
                        input = Console.ReadLine() ?? "";
                        if (input == null)
                        {
                            break;
                        }
                        manager.Remove(input);
                        break;
                    case "3":
                        Console.Write("请输入修改的学生姓名和新成绩：");
                        input = Console.ReadLine() ?? "";
                        if (input == null)
                        {
                            break;
                        }
                        parts = input.Split(",");
                        if (parts.Length != 2 || !int.TryParse(parts[1], out int score2))
                        {
                            Console.WriteLine("非法输入！");
                        }
                        else
                        {
                            manager.Update(parts[0], score2);
                        }
                        break;
                    case "4":
                        Console.Write("请输入查询的学生姓名：");
                        input = Console.ReadLine() ?? "";
                        if (input == null)
                        {
                            break;
                        }
                        manager.Find(input);
                        break;
                    case "5":
                        manager.PrintAll();
                        break;
                    case "6":
                        manager.PrintAllScore();
                        break;
                    case "7":
                        manager.PrintStats();
                        break;
                }
            }
        }
    }
}