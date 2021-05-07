using System.Diagnostics;
using iTechArt.SurveyCreator.Models;
using iTechArt.SurveyCreator.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iTechArt.SurveyCreator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UnitOfWork UnitOfOfWork { get; }


        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            UnitOfOfWork = new UnitOfWork(context);
        }


        public IActionResult Index()
        {
            _logger.Log(LogLevel.Information, "Index view");
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
