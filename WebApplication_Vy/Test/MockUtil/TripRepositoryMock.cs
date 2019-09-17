using System.Collections.Generic;
using Moq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace Test.MockUtil
{
    public static class TripRepositoryMock
    {
        public static readonly Mock<ITripRepository> MockTripRepository = new Mock<ITripRepository>();
        public static readonly List<Trip> Trips = new List<Trip>
        {
            new Trip() {TripId = 1, Route = "Oslo - Trondheim", Price = 100},
            new Trip() {TripId = 1, Route = "Oslo - Bergen", Price = 150},
            new Trip() {TripId = 1, Route = "Stavanger - Trondheim", Price = 200},
            new Trip() {TripId = 1, Route = "Fredrikstad - Bodø", Price = 250},
        };

        public static ITripRepository GetMockRepository()
        {
            MockTripRepository.Setup(mock => mock.FindAllTrips()).Returns(Trips);
            return MockTripRepository.Object;
        }
    }
}