using System;

namespace iTechArt.SurveyCreator.Repositories
{
    public class UnitOfWork: IDisposable, IUnitOfWork
    {
        private UserRepository _userRepository;
        private bool _disposed = false;


        public ApplicationContext ApplicationContext { get; }
        

        public UserRepository Users
        {
            get
            {
                if (_userRepository != null)
                {
                    return _userRepository;
                }

                return (_userRepository = new UserRepository(ApplicationContext));
            }
        }


        public UnitOfWork(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }


        public void Save()
        {
            ApplicationContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                ApplicationContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
