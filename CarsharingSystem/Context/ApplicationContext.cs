using System.Text.Json.Serialization;
using CarsharingSystem.Model;
using CarsharingSystem.Services;

namespace CarsharingSystem.Context;

public class ApplicationContext
{
    [JsonIgnore]
    public IPersistenceManager PersistenceManager { get; }

    public ClassExtent<User> Users { get; set; } = new();
    public ClassExtent<Vehicle> Vehicles { get; set; } = new();

    public ApplicationContext(IPersistenceManager persistenceManager)
    {
        PersistenceManager = persistenceManager;
    }
    public async Task LoadContextAsync()
    {
        if (File.Exists("context.json"))
        {
            var loadedContext = await PersistenceManager.LoadAsync("context.json");
            Users = loadedContext.Users;
            Vehicles = loadedContext.Vehicles;
        }
    }

    public async Task SaveAsync()
    {
        await PersistenceManager.SaveAsync(this, "context.json");
    }
}