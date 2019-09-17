using Moq;
using NUnit.Framework;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Service.Implementation;

namespace Test.Service
{
    [TestFixture]
    public class TripServiceImplTest
    {
        private ITripRepository _tripRepository = Mock<TripRepositoryImpl>;
        [SetUp]
        public void SetUp()
        {
            
        }
        
        [Test]
        public void GetAllTripDtos()
        {
            TripServiceImpl tripServiceImpl = new TripServiceImpl();
        }
    }
}