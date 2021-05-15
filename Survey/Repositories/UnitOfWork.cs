using System;
using System.Collections.Generic;
using iTechArt.Repositories;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Survey.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private bool _disposed; 

        private Dictionary<string, object> _repositories;

        private readonly DbContext _dbContext;


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
                return (Repository<T>) _repositories[type];
            
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);
            _repositories.Add(type, repositoryInstance);
            
            return (Repository<T>)_repositories[type];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext?.Dispose();
            }
            _disposed = true;
        }
    }
}
