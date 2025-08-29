using SQLite;

namespace HuesarioApp.Interfaces.DataServices;

public interface IRepository<T, in TY>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get (TY id);
    Task<int> Create (T entity);
    Task<int> Update (T entity);
    Task<int> Delete (TY id);
}