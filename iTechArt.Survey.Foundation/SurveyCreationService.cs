using System;
using System.Threading.Tasks;
using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.Survey.Foundation
{
    public class SurveyCreationService : ISurveyCreationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserProvider _currentUserProvider;


        public SurveyCreationService(IUnitOfWork unitOfWork,
                                     ICurrentUserProvider currentUserProvider)
        {
            _unitOfWork = unitOfWork;
            _currentUserProvider = currentUserProvider;
        }


        public async Task AddSurvey(Domain.Surveys.Survey survey)
        {
            var userId = _currentUserProvider.GetUserId();
            if (userId == null || userId == Guid.Empty)
            {
                throw new InvalidOperationException();
            }

            survey.CreatedById = (Guid)userId;

            _unitOfWork.GetRepository<Domain.Surveys.Survey>().Create(survey);

            await _unitOfWork.CommitAsync();
        }
    }
}
