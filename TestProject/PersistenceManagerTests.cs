using System.Text.Json;
using CarsharingSystem.Services;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace TestProject;

public class PersistenceManagerTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true
    };

    private class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }

    [Test]
    public void Save_SerializesListOfObjectsAndSavesToFile()
    {
        var cars = new List<Car>
        {
            new() { Make = "Toyota", Model = "Corolla", Year = 2020 },
            new() { Make = "Honda", Model = "Civic", Year = 2019 }
        };

        var fileName = Path.GetTempFileName();

        try
        {
            PersistenceManager.Save(cars, fileName);

            var json = File.ReadAllText(fileName);
            var deserializedCars = JsonSerializer.Deserialize<List<Car>>(json, _options);

            Assert.That(deserializedCars.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(deserializedCars[0].Make, Is.EqualTo("Toyota"));
                Assert.That(deserializedCars[0].Model, Is.EqualTo("Corolla"));
                Assert.That(deserializedCars[0].Year, Is.EqualTo(2020));
                Assert.That(deserializedCars[1].Make, Is.EqualTo("Honda"));
                Assert.That(deserializedCars[1].Model, Is.EqualTo("Civic"));
                Assert.That(deserializedCars[1].Year, Is.EqualTo(2019));
            });
        }
        finally
        {
            File.Delete(fileName);
        }
    }

    [Test]
    public void Load_DeserializesObjectsFromFile()
    {
        var cars = new List<Car>
        {
            new() { Make = "Toyota", Model = "Corolla", Year = 2020 },
            new() { Make = "Honda", Model = "Civic", Year = 2019 }
        };

        var fileName = Path.GetTempFileName();

        try
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(cars, _options));

            var loadedCars = PersistenceManager.Load<Car>(fileName);
            
            Assert.That(loadedCars, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(loadedCars[0].Make, Is.EqualTo("Toyota"));
                Assert.That(loadedCars[0].Model, Is.EqualTo("Corolla"));
                Assert.That(loadedCars[0].Year, Is.EqualTo(2020));
                Assert.That(loadedCars[1].Make, Is.EqualTo("Honda"));
                Assert.That(loadedCars[1].Model, Is.EqualTo("Civic"));
                Assert.That(loadedCars[1].Year, Is.EqualTo(2019));
            });
        }
        finally
        {
            File.Delete(fileName);
        }
    }

    [Test]
    public void Load_ThrowsException_WhenFileDoesNotExist()
    {
        var fileName = "nonexistentfile.json";
        Assert.Throws<FileNotFoundException>(() => { PersistenceManager.Load<Car>(fileName); });
    }
}