using System.Diagnostics;
using iTechArt.Survey.Configure;
using iTechArt.Survey.Models;
using iTechArt.Survey.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iTechArt.Survey.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Settings Settings{ get; }

        public UnitOfWork UnitOfOfWork { get; }
        

        public HomeController(ILogger<HomeController> logger, IOptions<Settings> settings, ApplicationContext context)
        {
            _logger = logger;
            Settings = settings.Value;
            UnitOfOfWork = new UnitOfWork(context);
            var check = UnitOfOfWork.Repository<User>();
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
