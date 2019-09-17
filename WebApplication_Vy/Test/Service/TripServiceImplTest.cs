using NUnit.Framework;
using Test.MockUtil;
using WebApplication_Vy.Service.Implementation;

namespace Test.Service
{
    [TestFixture]
    public class TripServiceImplTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void FindTripsMatching_shouldReturnEmptyList()
        {
            var tripServiceImpl =
                new TripServiceImpl(TripRepositoryMock.FindTripsMatching());
            var result = tripServiceImpl.FindTripsMatching("Lillehammer");

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void FindTripsMatching_shouldReturnListOfOneElement()
        {
            var tripServiceImpl =
                new TripServiceImpl(TripRepositoryMock.FindTripsMatching());
            var result = tripServiceImpl.FindTripsMatching("Stavanger");

            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void GetAllTripDtos_shouldReturnListOf4Elements()
        {
            var tripServiceImpl =
                new TripServiceImpl(TripRepositoryMock.GetAllTripDtos());
            var result = tripServiceImpl.GetAllTripDtos();

            Assert.AreEqual(result.Count, 4);
        }
    }
}