using System;
using iTechArt.Repositories.Repository;

namespace iTechArt.Survey.Repositories
{
    public interface ISurveyRepository : IRepository<Domain.Surveys.Survey>
    {
        Domain.Surveys.Survey GetSurveyIncludePagesAndQuestions(Guid id);
    }
}
