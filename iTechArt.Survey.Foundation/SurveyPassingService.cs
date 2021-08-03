using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Repositories.UnitOfWork;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Domain.Surveys;
using iTechArt.Survey.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Foundation
{
    public class SurveyPassingService : ISurveyPassingService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurveyUnitOfWork _surveyUnitOfWork;
        

        public SurveyPassingService(UserManager<User> userManager,
                                    SignInManager<User> signInManager, 
                                    IUnitOfWork unitOfWork, 
                                    ISurveyUnitOfWork surveyUnitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _surveyUnitOfWork = surveyUnitOfWork;
        }


        public Domain.Surveys.Survey FindSurveyOrReturnNull(Guid id)
        {
            var survey = _surveyUnitOfWork.GetSurveyRepository().Get(id);

            if (survey == null || (survey.Options & SurveyOptions.RandomOrderOfQuestions) == 0)
            {
                return survey;
            }

            foreach (var page in survey.Pages)
            {
                Shuffle(page.Questions);
                for (var i = 0; i < page.Questions.Count; i++)
                {
                    page.Questions[i].Number = i;
                }
            }

            return survey;
        }

        public List<AnswerError> GetErrorsIfNotAllRequiredQuestionsIsAnswered(Guid surveyId, List<UserAnswer> userAnswers)
        {
            var requiredAnswers = _surveyUnitOfWork.GetSurveyRepository().Get(surveyId).Pages
                .SelectMany(page => page.Questions)
                .Where(question => question.IsRequired)
                .ToList();

            return (from requiredAnswer in requiredAnswers where !userAnswers
                .Exists(userAnswer => userAnswer.QuestionId == requiredAnswer.Id) 
                select new AnswerError() {QuestionId = requiredAnswer.Id, ErrorMessage = "Require answer!"})
                .ToList();
        }

        public async Task SaveOrUpdateIfExistUserAnswers(Guid surveyId, List<UserAnswer> userAnswers)
        {
            var userId = await _userManager.Users
                .Where(user => user.UserName == _signInManager.Context.User.Identity.Name)
                .Select(user => user.Id)
                .FirstOrDefaultAsync();

            var userAnswerRepository = _unitOfWork.GetRepository<UserAnswer>();

            if (userId != Guid.Empty)
            {
                var existAnswers = userAnswerRepository
                    .GetAll()
                    .Where(answer => answer.SurveyId == surveyId && answer.UserId == userId)
                    .ToList();

                foreach (var userAnswer in userAnswers)
                {
                    userAnswer.UserId = userId;
                    var existAnswer = existAnswers.FirstOrDefault(answer => answer.QuestionId == userAnswer.QuestionId);

                    if (existAnswer != null)
                    {
                        existAnswer.Answer = userAnswer.Answer;
                    }
                    else
                    {
                        userAnswerRepository.Create(userAnswer);
                    }
                }

                var surveyResult = _unitOfWork.GetRepository<SurveyResult>()
                    .GetAll().FirstOrDefault(result => result.SurveyId == surveyId && result.UserId == userId);

                if (surveyResult != null)
                {
                   surveyResult.CompletionDate = DateTime.Now;
                }
                else
                {
                    _unitOfWork.GetRepository<SurveyResult>().Create(new SurveyResult()
                    {
                        SurveyId = surveyId,
                        UserId = userId,
                        CompletionDate = DateTime.Now
                    });
                }
            }
            else
            {
                foreach (var userAnswer in userAnswers)
                {
                    userAnswerRepository.Create(userAnswer);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        public List<UserAnswer> GetExistingUserAnswers(Guid surveyId)
        {
            var userId = _userManager.Users
                .Where(user => user.UserName == _signInManager.Context.User.Identity.Name)
                .Select(user => user.Id)
                .FirstOrDefault();

            if (userId == Guid.Empty)
            {
                return new List<UserAnswer>();
            }

            var userAnswerRepository = _unitOfWork.GetRepository<UserAnswer>();

            return userAnswerRepository.GetAll()
                .Where(userAnswer => userAnswer.UserId == userId && userAnswer.SurveyId == surveyId)
                .ToList();
        }

        public bool UserIsAuthenticated()
        {
            return _signInManager.Context.User.Identity is {IsAuthenticated: true};
        }


        private static void Shuffle<T>(IList<T> list)
        {
            var rand = new Random();

            for (var i = list.Count - 1; i >= 1; i--)
            {
                var j = rand.Next(i + 1);

                var tmp = list[j];
                list[j] = list[i];
                list[i] = tmp;
            }
        }
    }
}