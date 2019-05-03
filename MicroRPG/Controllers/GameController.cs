using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroRPG.Controllers
{
    public class GameController : Controller
    {
        [Route("Main")]
        public IActionResult Main()
        {
            return View ();
        }

        [Route("PlayerDetails")]
        public IActionResult _PlayerDetails()
        {
            return PartialView();
        }

        public IActionResult _Creatures()
        {
            return PartialView();
        }

        public IActionResult _CreatureDetails()
        {
            return PartialView();
        }

        public IActionResult _Obstacles()
        {
            return PartialView();
        }

        public IActionResult _Puzzles()
        {
            return PartialView();
        }
    }
}