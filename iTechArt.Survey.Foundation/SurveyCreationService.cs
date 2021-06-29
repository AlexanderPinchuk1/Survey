using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Question;
using iTechArt.Survey.Domain.Survey;

namespace iTechArt.Survey.Foundation
{
    public class SurveyCreationService : ISurveyCreationService
    {
        private readonly List<Page> _pages;
        
        private Guid _activePage;
        private Guid _activeQuestion;
        
        private bool _isAnonymous;
        private bool _isRandomOrderOfQuestions;
        private bool _isQuestionHaveNumber;
        private bool _isPageHaveNumber;
        private bool _isRequiredFieldsAsterisks;
        private bool _isProgressBarExist;


        public SurveyCreationService()
        {
            _isRequiredFieldsAsterisks = false;
            _isProgressBarExist = false;
            _isPageHaveNumber = false;
            _isQuestionHaveNumber = false;
            _isAnonymous = false;
            _isRandomOrderOfQuestions = false;

            _pages = new List<Page>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Page1",
                    Questions = new List<Question>(),
                }
            };
            _activePage = _pages[0].Id;
            _activeQuestion = Guid.Empty;
        }


        public void AddPage()
        {
            var page =  new Page()
            {
                Id = Guid.NewGuid(),
                Name = "Page" + Convert.ToString(_pages.Count + 1),
                Questions = new List<Question>()
            };

            _pages.Add(page);
            _activePage = page.Id;
        }

        public List<Page> GetPages()
        {
            return _pages.GetRange(0, _pages.Count);
        }

        public void AddQuestion(Guid pageId, QuestionType type)
        {
            switch (type)
            {
                case QuestionType.WithOneAnswer or QuestionType.WithManyAnswers:
                {
                    var questionWithAnswers = new QuestionWithAnswers(type);
                    _pages.First(page => page.Id == pageId).Questions.Add(questionWithAnswers);
                    _activeQuestion = questionWithAnswers.Id;

                    break;
                }
                case QuestionType.WithRating or QuestionType.WithScale or QuestionType.WithFileInput or QuestionType
                    .WithTextAreaEditor:
                {
                    var question = new Question(type);
                    _pages.First(page => page.Id == pageId).Questions.Add(question);
                    _activeQuestion = question.Id;

                    break;
                }
            }

            _activePage = pageId;
        }

        public void EditQuestion(Question question)
        {
            var pagesWithQuestion = _pages.Where(page => page.Questions.Exists(questionInPage => questionInPage.Id == question.Id))
                .Select(page => new
                {
                    pageId = page.Id,
                    editQuestion = page.Questions.First(questionInPage => questionInPage.Id == question.Id)
                });

            foreach (var pageWithQuestion in pagesWithQuestion)
            {
                pageWithQuestion.editQuestion.Description = question.Description;
                pageWithQuestion.editQuestion.IsEdit = false;
                pageWithQuestion.editQuestion.IsRequired = question.IsRequired;

                _activePage = pageWithQuestion.pageId;
            }

            _activeQuestion = question.Id;
        }

        public void EditQuestionWithAnswers(QuestionWithAnswers question)
        {
            var pagesWithQuestion = _pages.Where(page => page.Questions.Exists(questionInPage => questionInPage.Id == question.Id))
                .Select(page => new
                {
                    pageId = page.Id,
                    editQuestion = page.Questions.First(questionInPage => questionInPage.Id == question.Id)
                });

            foreach (var pageWithQuestion in pagesWithQuestion)
            {
                if (pageWithQuestion.editQuestion is QuestionWithAnswers questionWithAnswers)
                {
                    questionWithAnswers.Answers = question.Answers;
                }

                pageWithQuestion.editQuestion.Description = question.Description;
                pageWithQuestion.editQuestion.IsEdit = false;
                pageWithQuestion.editQuestion.IsRequired = question.IsRequired;

                _activePage = pageWithQuestion.pageId;
            }

            _activeQuestion = question.Id;
        }

        public Guid GetActivePage()
        {
            return _activePage;
        }

        public Guid GetActiveQuestion()
        {
            return _activeQuestion;
        }

        public void ChangeSurveyType(SurveyType type)
        {
            switch (type) {
                case SurveyType.Anonymous:
                {
                    _isAnonymous = !_isAnonymous;

                    break;
                }
                case SurveyType.RandomOrderOfQuestions:
                {
                    _isRandomOrderOfQuestions = !_isRandomOrderOfQuestions;
                    break;
                }
                case SurveyType.QuestionHaveNumber:
                {
                    _isQuestionHaveNumber = !_isQuestionHaveNumber;
                    break;
                }
                case SurveyType.PageHaveNumber:
                {
                    _isPageHaveNumber = !_isPageHaveNumber;
                    break;
                }
                case SurveyType.RequiredFieldsAsterisks:
                {
                    _isRequiredFieldsAsterisks = !_isRequiredFieldsAsterisks;
                    break;
                }
                case SurveyType.ProgressBarExist:
                {
                    _isProgressBarExist = !_isProgressBarExist;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public SurveyTypes GetSurveyTypes()
        {
            return new()
            {
                IsAnonymous = _isAnonymous,
                IsRequiredFieldsAsterisks = _isRequiredFieldsAsterisks,
                IsProgressBarExist = _isProgressBarExist,
                IsPageHaveNumber = _isPageHaveNumber,
                IsQuestionHaveNumber = _isQuestionHaveNumber,
                IsRandomOrderOfQuestions = _isRandomOrderOfQuestions
            };
        }

        public void DeletePage(Guid pageId)
        {
            var index = _pages.FindIndex(page => page.Id == pageId);
            _pages.RemoveAt(index);

            _activePage = Guid.Empty;
  
        }

        public void ChangeQuestionPosition(Guid questionId, bool up)
        {
            var curPage = _pages.First(page => page.Questions.Exists(question => question.Id == questionId));
            var curQuestion = curPage.Questions.First(question => question.Id == questionId);
            var curIndex = curPage.Questions.IndexOf(curQuestion);

            if (up)
            {
                if (curIndex <=  0)
                {
                    return;
                }

                Swap(curPage.Questions, curIndex, curIndex-1);
            }
            else
            {
                if (curIndex >= curPage.Questions.Count - 1)
                {
                    return;
                }

                Swap(curPage.Questions, curIndex, curIndex + 1);
            }

            _activeQuestion = questionId;
        }

        private static void Swap(IList<Question> list, int indexA, int indexB)
        {
            var tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public void ChangePageName(Guid pageId, string pageName)
        {
            var pages = _pages.Where(page => page.Id == pageId).ToList();
            foreach (var page in pages)
            {
                page.Name = pageName;
            }

            _activePage = pageId;
        }

        public void SetEditQuestion(Guid questionId)
        {
            var pagesWithQuestion = _pages.Where(page => page.Questions.Exists(questionInPage => questionInPage.Id == questionId))
                .Select(page => new
                {
                    pageId = page.Id,
                    editQuestion = page.Questions.First(questionInPage => questionInPage.Id == questionId)
                });


            foreach (var pageWithQuestion in  pagesWithQuestion)
            {
                pageWithQuestion.editQuestion.IsEdit = true;
                _activeQuestion = pageWithQuestion.editQuestion.Id;
                _activePage = pageWithQuestion.pageId;
            }
        }

        public void CancelEditQuestion(Guid questionId)
        {
            foreach (var question in _pages.SelectMany(page => page.Questions.Where(question => question.Id == questionId)))
            {
                question.IsEdit = false;
                _activeQuestion = question.Id;
            }
        }

        public void DeleteQuestion(Guid questionId)
        {
            foreach (var page in _pages)
            {
                var questions = page.Questions.Where(question => question.Id == questionId).ToList();
                foreach (var question in questions)
                {
                    page.Questions.Remove(question);
                    _activePage = page.Id;
                }
            }

            _activeQuestion = Guid.Empty;
        }
    }
}
