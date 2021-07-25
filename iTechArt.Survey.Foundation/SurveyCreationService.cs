using System.Threading.Tasks;
using iTechArt.Repositories.UnitOfWork;
using iTechArt.Survey.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Survey.Foundation
{
    public class SurveyCreationService: ISurveyCreationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;


        public SurveyCreationService(UserManager<User> userManager,
                                     SignInManager<User> signInManager,
                                     IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task AddSurvey(Domain.Surveys.Survey survey)
        {
            survey.CreatedBy = await _userManager.GetUserAsync(_signInManager.Context.User);

            _unitOfWork.GetRepository<Domain.Surveys.Survey>().Create(survey);

            await _unitOfWork.CommitAsync();
        }
    }
}
