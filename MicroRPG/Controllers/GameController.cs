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

        [Route("Creatures")]
        public IActionResult _Creatures()
        {
            return View();
        }

        [Route("Creatures/{id}")]
        public IActionResult _CreatureDetails(string id)
        {
            return PartialView(nameof(_CreatureDetails), id);
        }

        [Route("Obstacles")]
        public IActionResult _Obstacles()
        {
            return View();
        }

        [Route("Obstacles/{id}")]
        public IActionResult _ObstacleDetails(string id)
        {
            return PartialView(nameof(_ObstacleDetails), id);
        }

        [Route("Puzzles")]
        public IActionResult _Puzzles()
        {
            return View();
        }
    }
}