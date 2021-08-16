using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Repositories.UnitOfWork;
using iTechArt.Survey.Domain;
using iTechArt.Survey.Domain.Surveys;
using iTechArt.Survey.Repositories;

namespace iTechArt.Survey.Foundation
{
    public class SurveyService : ISurveyService
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurveyUnitOfWork _surveyUnitOfWork;
        

        public SurveyService(ICurrentUserProvider currentUserProvider,
                                    IUnitOfWork unitOfWork, 
                                    ISurveyUnitOfWork surveyUnitOfWork)
        {
            _currentUserProvider = currentUserProvider;
            _unitOfWork = unitOfWork;
            _surveyUnitOfWork = surveyUnitOfWork;
        }


        public Domain.Surveys.Survey FindSurveyOrReturnNull(Guid id)
        {
            var survey = _surveyUnitOfWork.GetSurveyRepository().GetSurveyIncludePagesAndQuestions(id);

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

        public async Task<List<AnswerError>> SaveOrUpdateIfExistUserAnswers(Guid surveyId, List<UserAnswer> userAnswers)
        {
            var errors = GetErrorsIfNotAllRequiredQuestionsIsAnswered(surveyId, userAnswers);
            if (errors.Count != 0)
            {
                return errors;
            }

            CreateOrUpdateExistingAnswers(surveyId, userAnswers);

            await _unitOfWork.CommitAsync();

            return null;
        }

        private List<AnswerError> GetErrorsIfNotAllRequiredQuestionsIsAnswered(Guid surveyId, List<UserAnswer> userAnswers)
        {
            var requiredAnswers = _surveyUnitOfWork.GetSurveyRepository().GetSurveyIncludePagesAndQuestions(surveyId).Pages
                .SelectMany(page => page.Questions)
                .Where(question => question.IsRequired)
                .ToList();

            return (from requiredAnswer in requiredAnswers
                    where !userAnswers
                        .Exists(userAnswer => userAnswer.QuestionId == requiredAnswer.Id)
                    select new AnswerError() { QuestionId = requiredAnswer.Id, ErrorMessage = "Require answer!" })
                .ToList();
        }

        private void CreateOrUpdateExistingAnswers(Guid surveyId, IEnumerable<UserAnswer> userAnswers)
        {
            var userId = _currentUserProvider.GetUserId();

            var userAnswerRepository = _unitOfWork.GetRepository<UserAnswer>();

            var userAnswersId = Guid.NewGuid();

            if (userId != null)
            {
                var existAnswers = userAnswerRepository
                    .GetAll()
                    .Where(answer => answer.SurveyId == surveyId && answer.UserId == userId)
                    .ToList();

                if (existAnswers.Count == 0)
                {
                    userAnswers = SetUserAnswersId(userAnswers, userAnswersId, userId);
                }

                foreach (var userAnswer in userAnswers)
                {
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
                        UserId = (Guid) userId,
                        CompletionDate = DateTime.Now
                    });
                }
            }
            else
            {
                userAnswers = SetUserAnswersId(userAnswers, userAnswersId, null);
            
                foreach (var userAnswer in userAnswers)
                {
                    userAnswerRepository.Create(userAnswer);
                }
            }
        }

        private static IEnumerable<UserAnswer> SetUserAnswersId(IEnumerable<UserAnswer> userAnswers, Guid id, Guid? userId)
        {
            return userAnswers.Select(userAnswer => new UserAnswer()
            {
                Answer = userAnswer.Answer,
                Id = id,
                QuestionId = userAnswer.QuestionId,
                SurveyId = userAnswer.SurveyId,
                UserId = userId
            });
        }

        public List<UserAnswer> GetExistingUserAnswers(Guid surveyId)
        {
            var userId = _currentUserProvider.GetUserId();

            if (userId == null)
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
            return _currentUserProvider.IsAuthenticated();
        }

        public async Task AddSurvey(Domain.Surveys.Survey survey)
        {
            var userId = _currentUserProvider.GetUserId();
            if (userId == null || userId == Guid.Empty)
            {
                throw new InvalidOperationException("Error getting user id!");
            }

            survey.CreatedById = (Guid)userId;

            _unitOfWork.GetRepository<Domain.Surveys.Survey>().Create(survey);

            await _unitOfWork.CommitAsync();
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