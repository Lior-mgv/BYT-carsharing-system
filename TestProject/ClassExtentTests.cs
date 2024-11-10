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
            PersistenceContext.Add(user);

            var extent = PersistenceContext.GetExtent<User>();
            Assert.Contains(user, extent);
        }

        [Test]
        public void Add_ShouldNotAddDuplicateObjectToExtent()
        {
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            PersistenceContext.Add(user);
            var count = PersistenceContext.GetExtent<User>().Count;
            PersistenceContext.Add(user);

            var extent = PersistenceContext.GetExtent<User>();
            Assert.That(extent.Count, Is.EqualTo(count));
        }

        [Test]
        public void LoadContext_ShouldPopulateExtentFromFile()
        {
            var user = new User("Jane", "Smith", "jane.smith@example.com", "9876543210", null, null);
            PersistenceContext.Add(user);

            PersistenceContext.SaveContext();
            PersistenceContext.LoadContext();

            var extent = PersistenceContext.GetExtent<User>();
            Assert.IsTrue(extent.Exists(u => u.Email == "jane.smith@example.com"));
        }
        
        [Test]
        public void GetExtent_ShouldReturnDefensiveCopy_NotAllowExternalModification()
        {
            // Arrange: Add a User instance to the PersistenceContext
            var user = new User("John", "Doe", "john.doe@example.com", "1234567890", null, null);
            PersistenceContext.Add(user);

            // Act: Retrieve the extent and attempt to modify it
            var userExtent = PersistenceContext.GetExtent<User>();
            userExtent.Add(new User("Jane", "Smith", "jane.smith@example.com", "0987654321", null, null));

            // Assert: Verify that the original extent in PersistenceContext is unaffected
            var internalExtent = PersistenceContext.GetExtent<User>();
            Assert.That(internalExtent.Count, Is.EqualTo(1), "The internal extent should not be affected by external modifications.");
            Assert.That(internalExtent[0].Email, Is.EqualTo("john.doe@example.com"));
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
