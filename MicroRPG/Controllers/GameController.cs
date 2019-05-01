using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroRPG.Controllers
{
    public class GameController : Controller
    {
        [Route("environments")]
        public IActionResult Index()
        {
            return Content ("Nu 'är vi i game controllern");
        }
    }
}