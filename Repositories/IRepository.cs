using System.Collections.Generic;

namespace iTechArt.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T user);
        void Update(T user);
        void Delete(int id);
    }
}
