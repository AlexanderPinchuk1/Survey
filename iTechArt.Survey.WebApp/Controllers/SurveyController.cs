using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Surveys;
using iTechArt.Survey.Foundation;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Survey.WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;


        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }


        public IActionResult SurveyPassing(Guid surveyId)
        {
            if (surveyId == Guid.Empty)
            {
                return BadRequest();
            }

            var survey = _surveyService.FindSurveyOrReturnNull(surveyId);

            if (survey == null)
            {
                return NotFound();
            }

            if ((survey.Options & SurveyOptions.Anonymity) != 0 &&  !_surveyService.UserIsAuthenticated())
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnUrl = "/Survey/SurveyPassing?surveyId=" + surveyId
                });
            }

            var existingAnswers = _surveyService.GetExistingUserAnswers(surveyId) ;

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

            var answersErrors= await _surveyService.SaveOrUpdateIfExistUserAnswers(surveyId, userAnswers);
            if (answersErrors == null)
            {
                return Ok();
            }

            return BadRequest(answersErrors);
        }

        [Authorize]
        public IActionResult SurveyCreation()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddSurvey(Domain.Surveys.Survey survey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _surveyService.AddSurvey(survey);

            return Json(new { success = true });
        }
    }
}
