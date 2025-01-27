﻿using System.Collections;
using CarsharingSystem.Model;

namespace CarsharingSystem.Services;

public static class PersistenceContext
{
    private static readonly string Prefix = Environment.CurrentDirectory + @"\persistence\";
    private static readonly Dictionary<Type, IList> TypeToExtentMap = new()
    {
        {typeof(User), new List<User>()},
        {typeof(BoxVan), new List<BoxVan>()},
        {typeof(Truck), new List<Truck>()},
        {typeof(PassengerCar), new List<PassengerCar>()},
        {typeof(Address), new List<Address>()},
        {typeof(Booking), new List<Booking>()},
        {typeof(Offer), new List<Offer>()},
        {typeof(OfferReview), new List<OfferReview>()},
        {typeof(UserReview), new List<UserReview>()},
        {typeof(DiscountCode), new List<DiscountCode>()}
    };

    public static List<T>? GetExtent<T>()
    {
        return TypeToExtentMap.TryGetValue(typeof(T), out var list) ? new List<T>((List<T>)list) : null;
    }

    public static void DeleteFromExtent<T>(T obj)
    {
        if (!TypeToExtentMap.TryGetValue(typeof(T), out var list)) return;
        if (!list.Contains(obj)) return;
        list.Remove(obj);
    }

    public static void SaveContext()
    {
        Directory.CreateDirectory(Prefix);

        foreach (var key in TypeToExtentMap.Keys.OrderBy(type => type.Name))
        {
            PersistenceManager.Save(TypeToExtentMap[key], Prefix + key.Name + ".json");
        }
    }

    public static void LoadContext()
    {
        Directory.CreateDirectory(Prefix);
        foreach (var key in TypeToExtentMap.Keys.OrderBy(type => type.Name))
        {
            var fileName = Prefix + key.Name + ".json";
            if (!File.Exists(fileName)) continue;
            var deserializedList = typeof(PersistenceManager)
                .GetMethod("Load")!
                .MakeGenericMethod(key)
                .Invoke(null, [fileName]);
            if (deserializedList == null) return;
            foreach (var o in (IEnumerable)deserializedList)
            {
                TypeToExtentMap[key].Add(o);
            }
        } 
    }

    public static void AddToExtent<T>(T obj)
    {
        if (!TypeToExtentMap.TryGetValue(typeof(T), out var list)) return;
        if (!list.Contains(obj))
        {
            list.Add(obj);
        }
    }

    //Thought about adding this function when was finishing the tests. Then I understood, that I will need to redo all the tests. Not doing that. But still may be good to have it.
    // public static bool ContainsInExtent<T>(T obj)
    // {
    //     if (!TypeToExtentMap.TryGetValue(typeof(T), out var list)) return false;
    //     if (!list.Contains(obj)) return false;
    //     return true;
    // }
    
}