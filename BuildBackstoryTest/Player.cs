using System;
using System.Collections.Generic;
using System.Text;

namespace BuildBackstoryTest
{
    public class Player
    {
        public string Name { get; }

        public int Age { get; set; }

        public int StatGainCount { get; set; }  

        public List<Tag> Tags { get; set; }

        private static int idCount = 0;

        public int ID { get; }

        public Stats Stats { get; set; }

        //public int Attack { get; set; }
        //public int Speed { get; set; }
        //public int Wisdom { get; set; }
        //public int HP { get; set; }
        //public int MP { get; set; }
        //public int DamageReduction { get; set; }

        public Player(string name, int age)
        {
            Name = name;
            Age = age;
            StatGainCount = 0;
            ID = idCount++;

            Tags = new List<Tag>();

            Stats = new Stats
            {
                Attack = 0,
                Speed = 0,
                Wisdom = 0,
                HP = 0,
                MP = 0,
                DamageReduction = 0,
            };

        }

    }
}
