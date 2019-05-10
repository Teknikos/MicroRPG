using System;
using System.Collections.Generic;
using System.Text;

namespace MicroRPG.Models.Backstory
{
    public class Stats
    {
        public int Attack { get; set; }
        public int Speed { get; set; }
        public int Wisdom { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int DamageReduction { get; set; }

        public override string ToString()
        {
            return $"Attack: {Attack}, Speed: {Speed}, Wisdom: {Wisdom}, HP: {HP}, MP: {MP}, DamageReduction: {DamageReduction}";
        }

        public void AddStats(Stats stats)
        {
            Attack += stats.Attack;
            Speed += stats.Speed;
            Wisdom += stats.Wisdom;
            HP += stats.HP;
            MP += stats.MP;
            DamageReduction += stats.DamageReduction;
        }

        public void MultiplyStats(double n)
        {
            Attack = (int)Math.Round(n * Attack);
            Speed = (int)Math.Round(n * Speed);
            Wisdom = (int)Math.Round(n * Wisdom);
            HP = (int)Math.Round(n * HP);
            MP = (int)Math.Round(n * MP);
            DamageReduction = (int)Math.Round(n * DamageReduction);
        }
    }

}
