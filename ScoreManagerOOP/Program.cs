using System;
using System.Text.RegularExpressions;

namespace ScoreManager
{
    class Student
    {
        public Student(string name, int math, int english)
        {
            Name = name;
            Math = math;
            English = english;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"学生姓名：{Name}，数学成绩：{Math}，英语成绩：{English}");
        }
        public string Name
        {
            get; private set;
        }
        public int Math
        {
            get; private set;
        }
        public int English
        {
            get; private set;
        }
        public bool SetScore(int newmath, int newenglish)
        {
            if (newmath < 0 || newmath > 100 || newenglish < 0 || newenglish > 100)
            {
                return false;
            }
            else
            {
                Math = newmath;
                English = newenglish;
                return true;
            }
        }
        public override string ToString()
        {
            return $"{Name},{Math},{English}";
        }
        public static Student Parse(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 3)
            {
                if (int.TryParse(parts[1], out int math) && int.TryParse(parts[2], out int english))
                    return new Student(parts[0], math, english);
                else return null;
            }
            else return null;
        }
    }
    class ScoreManager
    {
        private List<Student> students = new List<Student>();
        public void Add(string name, int math, int english)
        {
            if (students.Any(s => s.Name == name))
            {
                Console.WriteLine("已存在该学生，添加失败！");
            }
            else if (math >= 0 && math <= 100 && english >= 0 && english <= 100)
            {
                Student student = new Student(name, math, english);
                students.Add(student);
                Console.WriteLine("已成功添加！");
            }
            else
            {
                Console.WriteLine("分数超出范围，添加失败！");
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
        public bool Update(string name, int newMath, int newEnglish)
        {
            if (students.Any(s => s.Name == name))
            {
                return students.Find(s => s.Name == name).SetScore(newMath, newEnglish);
            }
            else
            {
                return false;
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
            var scoreAlls = students.OrderByDescending(s => s.Math + s.English);
            foreach (var scoreAll in scoreAlls)
            {
                scoreAll.PrintInfo();
            }
        }
        public void PrintStats()
        {
            if (students.Count > 0)
            {
                double avg = students.Average(s => s.Math + s.English);
                double max = students.Max(s => s.Math + s.English);
                double min = students.Min(s => s.Math + s.English);
                Console.WriteLine($"平均值是{avg}，最大值是{max}，最小值是{min}");
            }
            else
            {
                Console.WriteLine("没有任何学生成绩！");
            }
        }
        public void PrintPassed()
        {
            foreach (var s in students.Where(s => s.Math >= 60 && s.English >= 60).OrderByDescending(s => s.Math + s.English))
            {
                s.PrintInfo();
            }
        }
        public void Save()
        {
            File.WriteAllLines("scoremanager.txt", students.Select(s => s.ToString()));
        }
        public void Load()
        {
            if (File.Exists("scoremanager.txt"))
            {
                foreach (string line in File.ReadAllLines("scoremanager.txt"))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    Student s = Student.Parse(line);
                    Student error = new Student ("错误", 0, 0 );
                    if(s != null)
                    students.Add(s);
                }
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
            manager.Load();
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
8. 查询及格学生
0. 退出程序
");
                input = Console.ReadLine() ?? "";
                if (input == null || input == "0")
                {
                    Console.WriteLine("已退出程序！");
                    manager.Save();
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
                        if (parts.Length != 3 || !int.TryParse(parts[1], out int math) || !int.TryParse(parts[2], out int english))
                        {
                            Console.WriteLine("非法输入！");
                        }
                        else
                        {
                            manager.Add(parts[0], math,english);
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
                        if (parts.Length != 3 || !int.TryParse(parts[1], out int score1) || !int.TryParse(parts[2], out int score2))
                        {
                            Console.WriteLine("非法输入！");
                        }
                        else
                        {
                            if(manager.Update(parts[0], score1, score2))
                            {
                                Console.WriteLine("已成功修改！");
                            }
                            else
                            {
                                Console.WriteLine("修改失败！");
                            }
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
                    case "8":
                        manager.PrintPassed();
                        break;
                }
            }
        }
    }
}