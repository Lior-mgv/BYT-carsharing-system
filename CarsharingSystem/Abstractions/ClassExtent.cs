using System.Text.Json;
using CarsharingSystem.Services;

namespace CarsharingSystem.Abstractions;

public abstract class ClassExtent<T> where T : ClassExtent<T>
{
    public static string filename = typeof(T).Name + ".json";
    protected static List<T> _objects = [];

    protected T DeepCopy()
    {
        var json = JsonSerializer.Serialize((T)this);
        return JsonSerializer.Deserialize<T>(json)!;
    }

    public static List<T> GetObjects()
    {
        return new List<T>(_objects);
    }

    public static void Save()
    {
        PersistenceManager.Save(_objects, filename);   
    }

    public static void Load()
    {
        if (!File.Exists(filename)) return;
        var loadedObjects = PersistenceManager.Load<T>(filename);
        foreach (var obj in loadedObjects)
        {
            _objects.Add(obj);
        }
    }
}