using System;

namespace MonsterBattle
{
    class Monster
    {
        public string Name
        {
            get;
            protected set;
        }
        public int HP
        {
            get;
            protected set;
        }
        public int ATK
        {
            get;
            protected set;
        }
        public Monster(string name, int HP, int ATK)
        {
            this.Name = name;
            this.HP = HP;
            this.ATK = ATK;
        }
        public virtual void Attack()
        {
            Console.WriteLine($"{Name}发动了进攻");
        }
        public virtual void Attack(Monster monster)
        {
            monster.HPDown(this);
            if(monster.HP > 0)
            {
                Console.WriteLine($"{monster.Name}当前生命值为{monster.HP}");
            }
            else
            {
                Console.WriteLine($"{monster.Name}已死亡");
            }
            
        }
        public void HPDown(Monster monster)
        {
            HP -= monster.ATK;
        }
    }
    class Silme : Monster
    {
        public Silme(string name, int HP, int ATK) : base(name, HP, ATK) { }
        public override void Attack()
        {
            Console.WriteLine($"{Name}发动了弹跳撞击");
        }
    }
    class Goblin : Monster
    {
        public Goblin(string name, int HP, int ATK) : base(name, HP, ATK) { }
        public override void Attack()
        {
            Console.WriteLine($"{Name}投出了长矛");
        }
    }
    class Dragon : Monster //Boss
    {
        private int HPmax;
        public Dragon(string name, int HP, int ATK) : base(name, HP, ATK)
        {
            this.ATK = 2 * ATK;
            this.HPmax = HP;
        }
        public override void Attack()
        {
            Console.WriteLine($"{Name}发动龙之吐息");
        }
        public override void Attack(Monster monster)
        {
            if (HP * 2 > HPmax)
            {
                Console.WriteLine($"{Name}发动了怒吼，伤害翻倍");
                for(int i = 0; i < 2; i++)
                {
                    monster.HPDown(this);
                }
                if (monster.HP > 0)
                    Console.WriteLine($"{monster.Name}当前生命值为{monster.HP}");
                else
                    Console.WriteLine($"{monster.Name}已死亡");
            }
            else
            {
                monster.HPDown(this);
                if (monster.HP > 0)
                    Console.WriteLine($"{monster.Name}当前生命值为{monster.HP}");
                else
                    Console.WriteLine($"{monster.Name}已死亡");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Monster> list = new List<Monster> { new Silme("火史莱姆", 10, 2),new Goblin("哥布林投矛手",6,2),new Silme("水史莱姆",7,3),new Goblin("精英哥布林投矛手",7,3),new Dragon("绒翼龙",10,3) };
            foreach(Monster monster in list)
            {
                monster.Attack();
            }
            list[4].Attack(list[3]);
        }
    }
}