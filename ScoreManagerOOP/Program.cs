using System;

namespace ScoreManager
{
    class Student
    {
        private string name;
        private int score;
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
        public void Remove(string name) { }
        public void Find()
        {
            foreach(var stu in students)
            {
                stu.PrintInfo();
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
5. 显示所有学生（按成绩从高到低）
6. 显示统计信息（平均分/最高分/最低分）
0. 退出程序
");
                input = Console.ReadLine() ?? "";
                if (input == null||input == "0")
                {
                    Console.WriteLine("已退出程序！");
                    break;
                }
                switch (input)
                {
                    case "1":
                        Console.WriteLine("请添加学生的姓名和成绩：");
                        input = Console.ReadLine() ?? "";
                        if (input == null)
                        {
                            break;
                        }
                        parts = input.Split(',');
                        if (parts.Length != 2 || !int.TryParse(parts[1], out int score))
                        {
                            Console.WriteLine("非法输入！");
                        }
                        else
                        {
                            manager.Add(parts[0], score);
                        }
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        manager.Find();
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                }
            }
        }
    }
}