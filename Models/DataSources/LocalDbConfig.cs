using HuesarioApp.Models.Entities;
using HuesarioApp.Settings;
using SQLite;

namespace HuesarioApp.Models.DataSources;

public class LocalDbConfig
{
    private readonly SQLiteAsyncConnection _connection = new(GlobalVariables.DbPath);

    public async Task MakeTables()
    {
        await _connection.CreateTableAsync<Brands>();
        await _connection.CreateTableAsync<VehicleModels>();
        await _connection.CreateTableAsync<Parts>();
        await _connection.CreateTableAsync<Sales>();
    }
}