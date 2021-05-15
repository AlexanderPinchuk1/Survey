using System;

namespace iTechArt.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
