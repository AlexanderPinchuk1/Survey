using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.Survey.Domain;

namespace iTechArt.Survey.Foundation
{
    public interface ISurveyService
    {
        public Domain.Surveys.Survey FindSurveyOrReturnNull(Guid id);

        public Task<List<AnswerError>> SaveOrUpdateIfExistUserAnswers(Guid surveyId, List<UserAnswer> userAnswers);

        public List<UserAnswer> GetExistingUserAnswers(Guid surveyId);

        public bool UserIsAuthenticated();

        public Task AddSurvey(Domain.Surveys.Survey survey);
    }
}
