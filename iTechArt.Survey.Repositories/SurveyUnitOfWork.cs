using iTechArt.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public class SurveyUnitOfWork : UnitOfWork
    {
        public SurveyUnitOfWork(DbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}