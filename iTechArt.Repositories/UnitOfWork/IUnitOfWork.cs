using iTechArt.Repositories.Repository;

namespace iTechArt.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        Repository<T> GetRepository<T>() where T : class;
    }
}
