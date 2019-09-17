using System.Collections.Generic;
using System.Linq;
using Moq;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.Entities;

namespace Test.MockUtil
{
    public class TripRepositoryMock
    {
        private static readonly List<Trip> Trips = new List<Trip>
        {
            new Trip {TripId = 1, Route = "Oslo - Trondheim", Price = 100},
            new Trip {TripId = 2, Route = "Oslo - Bergen", Price = 150},
            new Trip {TripId = 3, Route = "Stavanger - Trondheim", Price = 200},
            new Trip {TripId = 4, Route = "Fredrikstad - Bodø", Price = 250}
        };

        public static ITripRepository GetAllTripDtos()
        {
            var mockTripRepository = new Mock<ITripRepository>();
            mockTripRepository.Setup(mock => mock.FindAllTrips()).Returns(Trips);
            return mockTripRepository.Object;
        }

        public static ITripRepository FindTripsMatching()
        {
            var mockTripRepository = new Mock<ITripRepository>();
            mockTripRepository
                .Setup(mock => mock.TripSearch(It.IsAny<string>()))
                .Returns((string query) => Trips
                    .Where(trip => trip.Route.Contains(query))
                    .ToList());

            return mockTripRepository.Object;
        }
    }
}