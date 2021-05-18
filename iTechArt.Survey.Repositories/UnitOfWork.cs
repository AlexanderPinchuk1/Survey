using System;
using System.Collections.Generic;
using iTechArt.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.WebApp.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _dbContext;

        private IDictionary<string, object> _repositories;
        

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
         

        public void Commit()
        {
            _dbContext.SaveChanges();
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
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
            _repositories.Add(type, repositoryInstance);
            
            return (Repository<T>)_repositories[type];
        }
    }
}
