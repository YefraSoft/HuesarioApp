using HuesarioApp.Models.Entities;
namespace HuesarioApp.Models.DataServices;
using SQLite;
using HuesarioApp.Settings;

public class LocalDbConfig
{
    private readonly SQLiteAsyncConnection _connection = new(GlobalVariables.DbPath);

    public async Task MakeTables()
    {
        await _connection.CreateTableAsync<Brands>();
        await _connection.CreateTableAsync<Entities.Models>();
        await _connection.CreateTableAsync<Parts>();
        await _connection.CreateTableAsync<Sales>();
    }
}