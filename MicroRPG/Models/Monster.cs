using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models
{
    public class Monster
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Special { get; set; }
        public int Speed { get; set; }
        public int HP { get; set; }
        public int Reduction { get; set; }
        public string Damage { get; set; }
    }
}
