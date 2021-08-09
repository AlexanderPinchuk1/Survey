using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Surveys;
using iTechArt.Survey.Foundation;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Survey.WebApp.Controllers
{
    public class SurveyPassingController : Controller
    {
        private readonly ISurveyPassingService _surveyPassingService;


        public SurveyPassingController(ISurveyPassingService surveyPassingService)
        {
            _surveyPassingService = surveyPassingService;
        }


        public IActionResult Index(Guid surveyId)
        {
            if (surveyId == Guid.Empty)
            {
                return BadRequest();
            }

            var survey = _surveyPassingService.FindSurveyOrReturnNull(surveyId);

            if (survey == null)
            {
                return NotFound();
            }

            if ((survey.Options & SurveyOptions.Anonymity) != 0 &&  !_surveyPassingService.UserIsAuthenticated())
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnUrl = "/SurveyPassing/Index?surveyId=" + surveyId
                });
            }

            var existingAnswers = _surveyPassingService.GetExistingUserAnswers(surveyId) ;

            var model = new SurveyViewModel()
            {
                Id = survey.Id,
                Name = survey.Name,
                Options = survey.Options,
                Pages = survey.Pages.Select(page => new PageViewModel()
                {
                    Id = page.Id,
                    Name = page.Name,
                    Questions = page.Questions
                        .Select(question => new QuestionViewModel()
                        {
                            Question = question,
                            SurveyOptions = survey.Options,
                            Answer = existingAnswers.FirstOrDefault(existingAnswer => existingAnswer.QuestionId == question.Id)?.Answer
                        }).ToList()
                }).ToList()

            };

            return View(model);
        }

        public async Task<IActionResult> SaveAnswers(Guid surveyId, List<UserAnswer> userAnswers)
        {
            if (surveyId == Guid.Empty || userAnswers.Any(userAnswer => userAnswer.SurveyId != surveyId))
            {
                return BadRequest();
            }

            var answersErrors= await _surveyPassingService.SaveOrUpdateIfExistUserAnswers(surveyId, userAnswers);
            if (answersErrors == null)
            {
                return Ok();
            }

            return BadRequest(answersErrors);
        }
    }
}
