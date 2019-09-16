using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using WebApplication_Vy.Db;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Models.Entities;

namespace Test.Db.Repositories
{
    [TestFixture]
    public class TripRepositoryImplTest
    {
        [SetUp]
        public void setUp()
        {
            _data = new List<Trip>
            {
                new Trip {TripId = 1, Route = "Oslo-Trondheim", Price = 100},
                new Trip {TripId = 2, Route = "Trondheim-Oslo", Price = 150},
                new Trip {TripId = 3, Route = "Stavanger-Bergen", Price = 250}
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Trip>>();
            _mockSet.As<IQueryable<Trip>>().Setup(m => m.Provider).Returns(_data.Provider);
            _mockSet.As<IQueryable<Trip>>().Setup(m => m.Expression).Returns(_data.Expression);
            _mockSet.As<IQueryable<Trip>>().Setup(m => m.ElementType).Returns(_data.ElementType);

            _mockVyDbContext = new Mock<VyDbContext>();
            _mockVyDbContext.Setup(c => c.Trips).Returns(_mockSet.Object);
        }

        private Mock<DbSet<Trip>> _mockSet;
        private IQueryable _data;
        private Mock<VyDbContext> _mockVyDbContext;

        [Test]
        public void TripSearch_ShouldNotReturnNull()
        {
            ITripRepository tripRepository = new TripRepositoryImpl();
            tripRepository.FindAllTrips();


        }
    }
}