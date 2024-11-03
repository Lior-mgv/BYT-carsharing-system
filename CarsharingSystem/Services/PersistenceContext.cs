using CarsharingSystem.Model;

namespace CarsharingSystem.Services;

public static class PersistenceContext
{
    public static void SaveContext()
    {
        User.Save();
        Vehicle.Save();
        Offer.Save();
    }

    public static void LoadContext()
    {
        User.Load();
        Vehicle.Load();
        Offer.Load();
    }
}