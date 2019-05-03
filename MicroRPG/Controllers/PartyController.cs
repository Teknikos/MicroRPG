using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroRPG.Controllers
{
    public class PartyController : Controller
    {
        [Route("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult _CreateOrLoad()
        {
            return PartialView();
        }

        [Route("Load")]
        public IActionResult Load()
        {
            return View();
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Summary")]
        public IActionResult Summary()
        {
            return View();
        }

        [Route("Backstory")]
        public IActionResult Backstory()
        {
            return View();
        }

        [Route("Spells")]
        public IActionResult Spells()
        {
            return View();
        }
    }
}