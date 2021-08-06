using System.Threading.Tasks;

namespace iTechArt.Survey.Foundation
{
    public interface ISurveyCreationService
    { 
        public Task AddSurvey(Domain.Surveys.Survey survey);
    }
}
