using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRPG.Models.ViewModels
{
    public class GameMainVM
    {

        public string[] PlayerNames { get; set; }

        public int[] PlayerIDs { get; set; }
        public string CurrentEnvironment { get; set; }
    }
}
