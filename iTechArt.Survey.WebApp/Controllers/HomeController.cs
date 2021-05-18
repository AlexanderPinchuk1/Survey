using System.Diagnostics;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Repositories;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iTechArt.Survey.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Settings Settings { get; }
        

        public UnitOfWork UnitOfWork { get; }
        

        public HomeController(ILogger<HomeController> logger, IOptions<Settings> settings, UnitOfWork unitOfWork)
        {
            _logger = logger;
            Settings = settings.Value;
            UnitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            _logger.Log(LogLevel.Information, "Index view");
            ViewBag.ApplicationName = Settings.ApplicationName;

            return View();
        }

        public IActionResult About()
        {
            _logger.Log(LogLevel.Information, "About view");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.Log(LogLevel.Information, "Error view");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
