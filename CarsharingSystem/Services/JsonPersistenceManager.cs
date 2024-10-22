using System.Text.Json;
using CarsharingSystem.Context;

namespace CarsharingSystem.Services;

public class JsonPersistenceManager : IPersistenceManager
{
    public async Task SaveAsync(ApplicationContext context, string fileName)
    {
        await using var fileStream = File.Create(fileName);
        await JsonSerializer.SerializeAsync(fileStream, context);
    }

    public async Task<ApplicationContext> LoadAsync(string fileName)
    {
        await using FileStream fileStream = File.OpenRead(fileName);
        var deserializedContext = await JsonSerializer.DeserializeAsync<ApplicationContext>(fileStream);
        return deserializedContext;
    }
}