using System;
using System.Collections.Generic;

namespace iTechArt.Repositories.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T user);
        void Update(T user);
        void Delete(Guid id);
    }
}
