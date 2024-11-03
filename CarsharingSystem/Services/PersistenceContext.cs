using CarsharingSystem.Model;

namespace CarsharingSystem.Services;

public class PersistenceContext
{
    public static void SaveContext()
    {
        PersistenceManager.Save(User.GetObjects(), nameof(User) + ".json");
        PersistenceManager.Save(Vehicle.GetObjects(), nameof(Vehicle) + ".json");   
        PersistenceManager.Save(Offer.GetObjects(), nameof(Offer) + ".json");
    }

    public static void LoadContext()
    {
        LoadExtent<User>();
        LoadExtent<Vehicle>();
        LoadExtent<Offer>();
    }

    private static void LoadExtent<T>()
    {
        var filename = typeof(T).Name + ".json";
        if (File.Exists(filename))
        {
            PersistenceManager.Load<T>(filename);
        }
    }
}