using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroRPG.Controllers
{
    public class EnvironmentController : Controller
    {
        [Route("environment")]
        public IActionResult Choose()
        {
            return View();
        }
        public IActionResult Zoomed()
        {
            return PartialView();
        }
    }
}