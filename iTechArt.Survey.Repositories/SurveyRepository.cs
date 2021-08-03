using System;
using System.Collections.Generic;
using System.Linq;
using iTechArt.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public class SurveyRepository: IRepository<Domain.Surveys.Survey>
    {
        private readonly SurveyDbContext _surveyDbContext;
        private readonly DbSet<Domain.Surveys.Survey> _dbSet;


        public SurveyRepository(SurveyDbContext surveyDbContext)
        {
            _surveyDbContext = surveyDbContext;
            _dbSet = surveyDbContext.Set<Domain.Surveys.Survey>();
        }


        public IEnumerable<Domain.Surveys.Survey> GetAll()
        {
            return _dbSet;
        }

        public Domain.Surveys.Survey Get(Guid id)
        {
            return _dbSet.Include(survey => survey.Pages.OrderBy(page=> page.Number))
                    .ThenInclude(page => page.Questions.OrderBy(question => question.Number))
                    .First(survey => survey.Id == id);
        }

        public void Create(Domain.Surveys.Survey survey)
        {
            _dbSet.Add(survey);
        }

        public void Update(Domain.Surveys.Survey survey)
        {
            _surveyDbContext.Entry(survey).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var item = _dbSet.Find(id);
            if (item == null)
            {
                return;
            }

            _dbSet.Remove(item);
        }
    }
}
