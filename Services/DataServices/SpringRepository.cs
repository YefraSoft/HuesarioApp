using System.Text;
using System.Text.Json;
using HuesarioApp.Interfaces.DataServices;

namespace HuesarioApp.Services.DataServices;

public class SpringRepository<T, TY> : IRepository<T, TY>
{
    private readonly HttpClient _client;

    private SpringRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var response = await _client.GetAsync("/tickets");
        if (response.IsSuccessStatusCode)
            return new List<T>().AsEnumerable();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<T>>(json);
    }

    public Task<T> Get(TY id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(T entity)
    {
        var body = JsonSerializer.Serialize(entity);
        var content = new StringContent(body, Encoding.UTF8, @"application/json");
        var response = await _client.PostAsync("/tickets", content);

        response.EnsureSuccessStatusCode();

        var list = JsonSerializer.Deserialize<List<T>>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return list?.Count ?? 0;
    }

    public Task<int> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> Delete(TY id)
    {
        throw new NotImplementedException();
    }
}