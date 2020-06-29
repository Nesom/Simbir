using System;
using TestTask.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.Models;
using ProjectInformatics.Logging;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private IDatabase db;
        
        public HomeController(ILogger<HomeController> logger, IDatabase context)
        {
            db = LoggingAdvice<IDatabase>.Create(context, logger);
        }

        public IActionResult Index()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
