using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.Survey.Repositories
{
    public class SurveyUnitOfWork : UnitOfWork, ISurveyUnitOfWork
    {
        private SurveyRepository _surveyRepository;

        private readonly SurveyDbContext _surveyDbContext;
        

        public SurveyUnitOfWork(SurveyDbContext dbContext) 
            : base(dbContext)
        {
            _surveyDbContext = dbContext;
        }


        public ISurveyRepository GetSurveyRepository()
        {
            return _surveyRepository ??= new SurveyRepository(_surveyDbContext);
        }
    }
}