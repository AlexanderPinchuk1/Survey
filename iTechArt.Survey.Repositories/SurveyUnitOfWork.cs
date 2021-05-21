using System;
using System.Collections.Generic;
using iTechArt.Repositories.Repository;
using iTechArt.Repositories.UnitOfWork;

namespace iTechArt.Survey.Repositories
{
    internal class SurveyUnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repositories;

        private readonly SurveyDbContext _surveyDbContext;


        public SurveyUnitOfWork(SurveyDbContext surveyDbContext)
        {
            _surveyDbContext = surveyDbContext;
        }


        public void Commit()
        {
            _surveyDbContext.SaveChanges();
        }

        public Repository<T> Repository<T>() where T : class
        {
            _repositories ??= new Dictionary<string, object>();
            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type))
            {
                return (Repository<T>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _surveyDbContext);
            _repositories.Add(type, repositoryInstance);

            return (Repository<T>) _repositories[type];
        }
    }
}