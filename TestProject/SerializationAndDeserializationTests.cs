using CarsharingSystem.Model;
using CarsharingSystem.Services;

namespace TestProject;

public class SerializationAndDeserializationTests
{
    private const string TestFilePath = "test_offer.json";
    [SetUp]
    public void SetUp()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    [Test]
    public void Serialization_ShouldSerializeAndDeserializeCorrectly()
    {
        var vehicle = new PassengerCar();
        var offer = new Offer(100, "Description", 18, vehicle);

        // Serialize
        PersistenceManager.Save(new List<Offer> { offer }, TestFilePath);
        Assert.IsTrue(File.Exists(TestFilePath));
            
        // Deserialize
        var deserializedOffers = PersistenceManager.Load<Offer>(TestFilePath);
        Assert.That(deserializedOffers.Count, Is.EqualTo(1));

        var deserializedOffer = deserializedOffers[0];
        Assert.That(deserializedOffer.PricePerDay, Is.EqualTo(offer.PricePerDay));
        Assert.That(deserializedOffer.Description, Is.EqualTo(offer.Description));
        Assert.That(deserializedOffer.MinimalAge, Is.EqualTo(offer.MinimalAge));
        Assert.That(deserializedOffer.Vehicle.GetType(), Is.EqualTo(offer.Vehicle.GetType()));
    }
}