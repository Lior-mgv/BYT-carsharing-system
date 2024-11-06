using System.Collections;
using System.Text.Json;

namespace CarsharingSystem.Services;

public static class PersistenceManager
{
    private static JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        ReferenceHandler = new MyReferenceHandler()
    };

    public static void Save(IEnumerable objects, string fileName)
    {
        using var fileStream = File.Create(fileName);
        JsonSerializer.Serialize(fileStream, objects, options);
    }

    public static List<T> Load<T>(string fileName)
    {
        using var fileStream = File.OpenRead(fileName);
        return JsonSerializer.Deserialize<List<T>>(fileStream, options);
    }
}