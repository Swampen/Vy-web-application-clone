using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Test.MockUtil;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Models.DTO;
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
        public void GetAllTripDtos_should_return_list_of_4_elements()
        {
            var tripServiceImpl =
                new TripServiceImpl(TripRepositoryMock.GetAllTripDtos());
            List<TripDTO> result = tripServiceImpl.GetAllTripDtos();
            
            Assert.AreEqual(result.Count, 4);
        }

        [Test]
        public void FindTripsMatching_shouldReturnListOfOneElement()
        {
            var tripServiceImpl =
                new TripServiceImpl(TripRepositoryMock.FindTripsMatching());
            List<TripDTO> result = tripServiceImpl.FindTripsMatching("Stavanger");
            
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void FindTripsMatching_shouldReturnEmptyList()
        {
            var tripServiceImpl = 
                new TripServiceImpl(TripRepositoryMock.FindTripsMatching());
            List<TripDTO> result = tripServiceImpl.FindTripsMatching("Lillehammer");
            
            Assert.AreEqual(0, result.Count);
        }
    }
}