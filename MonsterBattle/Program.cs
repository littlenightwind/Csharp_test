using System;

namespace MonsterBattle
{
    interface IDamageable
    {
        int HP { get; }
        string Name { get; }
        bool IsDead { get; }
        void TakeDamage(int damage);
    }
    class Role : IDamageable
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
        public bool IsDead => HP <= 0;
        public Role(string name, int HP, int ATK)
        {
            this.Name = name;
            this.HP = HP;
            this.ATK = ATK;
        }
        public virtual void Attack()
        {
            Console.WriteLine($"{Name}发动了进攻");
        }
        public virtual void Attack(IDamageable target)
        {
            target.TakeDamage(ATK);
            if (!target.IsDead)
            {
                Console.WriteLine($"{target.Name}当前生命值为{target.HP}");
            }
            else
            {
                Console.WriteLine($"{target.Name}已死亡");
            }
        }
        public void TakeDamage(int damage)
        {
            HP -= damage;
        }
    }
    class Silme : Role
    {
        public Silme(string name, int HP, int ATK) : base(name, HP, ATK) { }
        public override void Attack()
        {
            Console.WriteLine($"{Name}发动了弹跳撞击");
        }
    }
    class Goblin : Role
    {
        public Goblin(string name, int HP, int ATK) : base(name, HP, ATK) { }
        public override void Attack()
        {
            Console.WriteLine($"{Name}投出了长矛");
        }
    }
    class Dragon : Role //Boss
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
        public override void Attack(IDamageable target)
        {
            if (HP * 2 > HPmax)
            {
                Console.WriteLine($"{Name}发动了怒吼，伤害翻倍");
                for (int i = 0; i < 2; i++)
                {
                    target.TakeDamage(ATK);
                }
                if (!target.IsDead)
                    Console.WriteLine($"{target.Name}当前生命值为{target.HP}");
                else
                    Console.WriteLine($"{target.Name}已死亡");
            }
            else
            {
                target.TakeDamage(ATK);
                if (!target.IsDead)
                    Console.WriteLine($"{target.Name}当前生命值为{target.HP}");
                else
                    Console.WriteLine($"{target.Name}已死亡");
            }
        }
    }
    class Player : Role, IDamageable
    {
        public Player(string name, int HP, int ATK) : base(name, HP, ATK) { }
        public void Attack(IDamageable target)
        {
            Console.WriteLine("玩家发动进攻");
            target.TakeDamage(ATK);
            if (!target.IsDead)
            {
                Console.WriteLine($"{target.Name}当前生命值为{target.HP}");
            }
            else
            {
                Console.WriteLine($"{target.Name}已死亡");
            }
        }
        public void TakeDamage(int damage)
        {
            HP -= damage;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Role> list = new List<Role> { new Silme("火史莱姆", 10, 2), new Goblin("哥布林投矛手", 6, 2), new Silme("水史莱姆", 7, 3), new Goblin("精英哥布林投矛手", 7, 3), new Dragon("绒翼龙", 10, 3), new Role("战士", 15, 2) };
            while (!list[5].IsDead && !list[4].IsDead)
            {
                list[5].Attack(list[4]);
                if (list[4].IsDead)
                {
                    Console.WriteLine("玩家获胜");
                    break;
                }
                list[4].Attack(list[5]);
                if (list[5].IsDead)
                {
                    Console.WriteLine("怪物获胜");
                    break;
                }
            }

        }
    }
}