using System.Text.Json.Serialization;

namespace CarsharingSystem.Context;

public class ClassExtent<T>
{
    [JsonInclude]
    public List<T> Items { get; private set; } = [];
    public void Add(T item) => Items.Add(item);

    public bool Remove(T item) => Items.Remove(item);

}