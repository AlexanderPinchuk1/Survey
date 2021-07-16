using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Survey.WebApp.Controllers
{
    public class SurveyCreationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
