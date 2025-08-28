using HuesarioApp.Interfaces.DataServices;
using SQLite;

namespace HuesarioApp.Models.DataServices;

public class LocalRepository<T> : IRepository<T> where T : class, new()
{
    
    private readonly SQLiteAsyncConnection _db = new SQLiteAsyncConnection(Settings.GlobalVariables.DbPath);
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _db.Table<T>().ToListAsync();
    }

    public async Task<T> Get(int id)
    {
        return await _db.FindAsync<T>(id);
    }

    public async Task<int> Create(T entity)
    {
        return await _db.InsertAsync(entity);
    }

    public async Task<int> Update(T entity)
    {
        return await _db.UpdateAsync(entity);
    }

    public async Task<int> Delete(int id)
    {
        var entity = await Get(id);
        return await _db.DeleteAsync(entity);
    }
}