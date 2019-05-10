using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroRPG.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRPG.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}