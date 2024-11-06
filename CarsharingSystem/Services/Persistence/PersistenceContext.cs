using System.Collections;
using System.Collections.Specialized;
using CarsharingSystem.Model;

namespace CarsharingSystem.Services;

public static class PersistenceContext
{
    private static readonly string Prefix = Environment.CurrentDirectory + @"\persistence\";
    private static readonly OrderedDictionary TypeToExtentMap = new()
    {
        {typeof(User), new List<User>()},
        {typeof(BoxVan), new List<BoxVan>()},
        {typeof(Truck), new List<Truck>()},
        {typeof(PassengerCar), new List<PassengerCar>()},
        {typeof(Address), new List<Address>()},
        {typeof(Booking), new List<Booking>()},
        {typeof(Offer), new List<Offer>()},
        {typeof(OfferReview), new List<OfferReview>()},
        {typeof(UserReview), new List<UserReview>()}
    };

    public static List<T>? GetExtent<T>()
    {
        return (List<T>?)TypeToExtentMap[typeof(T)];
    }

    public static void SaveContext()
    {
        Directory.CreateDirectory(Prefix);
        foreach (DictionaryEntry pair in TypeToExtentMap)
        {
            PersistenceManager.Save((IEnumerable)pair.Value!, Prefix + ((Type)pair.Key).Name + ".json");
        }
    }

    public static void LoadContext()
    {
        Directory.CreateDirectory(Prefix);
        foreach (DictionaryEntry pair in TypeToExtentMap)
        {
            var fileName = Prefix + ((Type)pair.Key).Name + ".json";
            if (!File.Exists(fileName)) continue;
            var typeParam = pair.Value!.GetType().GetGenericArguments()[0];
            var deserializedList = typeof(PersistenceManager)
                .GetMethod("Load")!
                .MakeGenericMethod(typeParam)
                .Invoke(null, [fileName]);
            if (deserializedList == null) return;
            foreach (var o in (IEnumerable)deserializedList)
            {
                ((IList)TypeToExtentMap[typeParam]!).Add(o);
            }
        } 
    }

    public static void Add<T>(T obj)
    {
        ((IList?)TypeToExtentMap[typeof(T)])?.Add(obj);
    }
}