using CarsharingSystem.Services;

namespace CarsharingSystem.Abstractions;

public abstract class ClassExtent<T> where T : ClassExtent<T>
{
    private static readonly string Filename = typeof(T).Name + ".json";
    private static List<T> _objects = [];

    protected ClassExtent()
    {
        _objects.Add((T)this);
    }
    
    public static List<T> GetObjects()
    {
        if (File.Exists(Filename))
        {
            Load();
        }

        return new List<T>(_objects);
    }

    public static void Persist()
    {
        PersistenceManager.Save(_objects, Filename);
    }

    private static void Load()
    {
        PersistenceManager.Load<T>(Filename);
    }
}