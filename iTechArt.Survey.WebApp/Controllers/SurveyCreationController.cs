using System;
using iTechArt.Survey.Domain.Question;
using iTechArt.Survey.Domain.Survey;
using iTechArt.Survey.Foundation;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Survey.WebApp.Controllers
{
    public class SurveyCreationController : Controller
    {
        private readonly ISurveyCreationService _pagesService;


        public SurveyCreationController(ISurveyCreationService pagesService)
        {
            _pagesService = pagesService;
        }

        
        public IActionResult Index()
        {
            var model = new NewSurveyViewModel()
            {
                Pages = _pagesService.GetPages(),
                ActivePage = _pagesService.GetActivePage(),
                ActiveQuestion =  _pagesService.GetActiveQuestion(),
                SurveyTypes = _pagesService.GetSurveyTypes()
            };

            return View(model);
        }

        public IActionResult AddNewPage()
        {
            _pagesService.AddPage();

            return RedirectToAction("Index");
        }

        public IActionResult CancelEditQuestion(Guid questionId)
        {
            _pagesService.CancelEditQuestion(questionId);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteQuestion(Guid questionId)
        {
            _pagesService.DeleteQuestion(questionId);

            return RedirectToAction("Index");
        }

        public IActionResult AddQuestion(Guid activePageId, QuestionType questionType)
        {
            if (activePageId != Guid.Empty)
            {
                _pagesService.AddQuestion(activePageId, questionType);
            }

            return RedirectToAction("Index");
        }

        public IActionResult SetEditQuestion(Guid questionId)
        {
            _pagesService.SetEditQuestion(questionId);

            return RedirectToAction("Index");
        }

        public IActionResult EditQuestion(Question question)
        {
            _pagesService.EditQuestion(question);

            return RedirectToAction("Index");
        }

        public IActionResult EditQuestionWithAnswers(QuestionWithAnswers question)
        {
            _pagesService.EditQuestionWithAnswers(question);

            return RedirectToAction("Index");
        }

        public IActionResult ChangeSurveyType(SurveyType type)
        {
            _pagesService.ChangeSurveyType(type);

            return RedirectToAction("Index");
        }

        public IActionResult DeletePage(Guid pageId)
        {
            _pagesService.DeletePage(pageId);

            return RedirectToAction("Index");
        }

        public IActionResult ChangeQuestionPosition(Guid questionId, bool up)
        {
            _pagesService.ChangeQuestionPosition(questionId, up);

            return RedirectToAction("Index");
        }
    }
}
