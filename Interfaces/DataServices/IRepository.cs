using SQLite;

namespace HuesarioApp.Interfaces.DataServices;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get (int id);
    Task<int> Create (T entity);
    Task<int> Update (T entity);
    Task<int> Delete (int id);
}