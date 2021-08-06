using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.Survey.Repositories
{
    public class SurveyUnitOfWork : UnitOfWork
    {
        public SurveyUnitOfWork(SurveyDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}