using System.Threading.Tasks;
using iTechArt.Repositories.Repository;

namespace iTechArt.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Repository<T> GetRepository<T>() where T : class;
    }
}
