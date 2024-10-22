using CarsharingSystem.Context;

namespace CarsharingSystem.Services;

public interface IPersistenceManager
{
    public Task SaveAsync (ApplicationContext context, string fileName);
    public Task<ApplicationContext> LoadAsync(string fileName);
}