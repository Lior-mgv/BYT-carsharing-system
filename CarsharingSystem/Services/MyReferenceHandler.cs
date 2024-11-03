using System.Text.Json.Serialization;

namespace CarsharingSystem.Services;

public class MyReferenceHandler : ReferenceHandler
{
    public MyReferenceHandler() => Reset();
    private ReferenceResolver? _rootedResolver;
    public override ReferenceResolver CreateResolver() => _rootedResolver!;
    public void Reset() => _rootedResolver = new MyReferenceResolver();
}