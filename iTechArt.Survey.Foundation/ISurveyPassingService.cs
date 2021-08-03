using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;

namespace iTechArt.Survey.Foundation
{
    public interface ISurveyPassingService
    {
        public Domain.Surveys.Survey FindSurveyOrReturnNull(Guid id);

        public List<AnswerError> GetErrorsIfNotAllRequiredQuestionsIsAnswered(Guid surveyId, List<UserAnswer> userAnswers);

        public Task SaveOrUpdateIfExistUserAnswers(Guid surveyId, List<UserAnswer> userAnswers);

        public List<UserAnswer> GetExistingUserAnswers(Guid surveyId);

        public bool UserIsAuthenticated();
    }
}
