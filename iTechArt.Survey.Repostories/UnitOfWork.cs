using System;
using System.Collections.Generic;
using iTechArt.Repositories;

namespace iTechArt.Survey.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repositories;

        private readonly ApplicationContext _applicationContext;


        public UnitOfWork(ApplicationContext applicationContext )
        {
            _applicationContext = applicationContext;
        }


        public void Commit()
        {
            _applicationContext.SaveChanges();
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
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _applicationContext);
            _repositories.Add(type, repositoryInstance);

            return (Repository<T>) _repositories[type];
        }
    }
}