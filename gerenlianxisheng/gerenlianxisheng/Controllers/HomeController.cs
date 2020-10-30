using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using gerenlianxisheng.Controllers.tool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gerenlianxisheng.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //法一，ViewBag特性是在作用域内才有效，因此不能写在ajax控制器中，且所有页面都要写这一段
            ComHttpContext session = new ComHttpContext();
            string result = session.Get("logtype");
            ViewBag.session = result!=null ? result: "0";
            return View();
        }
        public IActionResult jiluyemian()
        {
            //法一，ViewBag特性是在作用域内才有效，因此不能写在ajax控制器中，且所有页面都要写这一段
            ComHttpContext session = new ComHttpContext();
            string result = session.Get("logtype");
            ViewBag.session = result != null ? result : "0";
            return View();
        }
    }
}
