using System.Threading.Tasks;
using iTechArt.Survey.Foundation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Survey.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SurveyCreationController : Controller
    {
        private readonly ISurveyCreationService _surveyCreationService;


        public SurveyCreationController(ISurveyCreationService surveyCreationService)
        {
            _surveyCreationService = surveyCreationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddSurvey(Domain.Surveys.Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _surveyCreationService.AddSurvey(survey);

            return Json(new { success = true });
        }
    }
}
