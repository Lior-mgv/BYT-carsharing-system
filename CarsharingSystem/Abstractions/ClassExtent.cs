using System.Text.Json;

namespace CarsharingSystem.Abstractions;

public abstract class ClassExtent<T> where T : ClassExtent<T>
{
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
}