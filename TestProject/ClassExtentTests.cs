using CarsharingSystem.Model;
using CarsharingSystem.Services;

namespace TestProject
{
    [TestFixture]
    public class ClassExtentTests
    {
        private static readonly string TestPrefix = Path.Combine(Environment.CurrentDirectory, "test_persistence");

        [SetUp]
        public void Setup()
        {
            // Clear out the test persistence directory
            if (Directory.Exists(TestPrefix))
            {
                Directory.Delete(TestPrefix, true);
            }
            Directory.CreateDirectory(TestPrefix);
        }

        [Test]
        public void GetExtent_ShouldReturnList_WhenTypeExists()
        {
            var userExtent = PersistenceContext.GetExtent<User>();
            Assert.NotNull(userExtent);
            Assert.IsInstanceOf<List<User>>(userExtent);
        }

        [Test]
        public void GetExtent_ShouldReturnNull_WhenTypeDoesNotExist()
        {
            var extent = PersistenceContext.GetExtent<Vehicle>();
            Assert.IsNull(extent);
        }

        [Test]
        public void Add_ShouldAddObjectToExtent_WhenTypeExists()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);

            var extent = PersistenceContext.GetExtent<User>();
            Assert.Contains(user, extent);
        }

        [Test]
        public void Add_ShouldNotAllowExternalModification()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            var count = PersistenceContext.GetExtent<User>()!.Count;
            PersistenceContext.AddToExtent(user); //trying to add the object for the second time

            var extent = PersistenceContext.GetExtent<User>();
            Assert.That(extent.Count, Is.EqualTo(count));
        }

        [Test]
        public void LoadContext_ShouldPopulateExtentFromFile()
        {
            var user = new User("Jane", "Smith", "jane.smith@example.com", "9876543210", null, null);

            PersistenceContext.SaveContext();
            PersistenceContext.LoadContext();

            var extent = PersistenceContext.GetExtent<User>();
            Assert.That(extent.Count(u => u.Email == "jane.smith@example.com"), Is.EqualTo(2));
        }
        
        [TearDown]
        public void Teardown()
        {
            // Clean up after tests
            if (Directory.Exists(TestPrefix))
            {
                Directory.Delete(TestPrefix, true);
            }
        }
    }
}
