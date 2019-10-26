using System.Collections.Generic;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using Moq;

namespace Test.MockUtil.RepositoryMock
{
    public class StationRepositoryMock
    {
        private static readonly List<Station> _stations = new List<Station>
        {
            new Station {Id = 1, Name = "1", StopId = "1"},
            new Station {Id = 2, Name = "2", StopId = "2"},
            new Station {Id = 3, Name = "3", StopId = "3"},
            new Station {Id = 4, Name = "4", StopId = "4"}
        };

        public static IStationRepository CreateStationMock()
        {
            var mockRepo = new Mock<IStationRepository>();
            mockRepo.Setup(mock => mock
                    .CreateStation(It.Is<Station>(station => station.Name.Equals("false"))))
                .Returns(false);
            mockRepo.Setup(mock => mock
                    .CreateStation(It.Is<Station>(station => station.Name.Equals("true"))))
                .Returns(true);
            return mockRepo.Object;
        }

        public static IStationRepository FindAllStationsMock()
        {
            var mockRepo = new Mock<IStationRepository>();
            mockRepo.Setup(mock => mock.FindAllStations()).Returns(_stations);
            return mockRepo.Object;
        }

        public static IStationRepository UpdateStationMock()
        {
            var mockRepo = new Mock<IStationRepository>();
            mockRepo.Setup(mock => mock.UpdateStation(It.IsAny<Station>())).Returns(true);
            return mockRepo.Object;
        }

        public static IStationRepository DeleteStationMock()
        {
            var mockRepo = new Mock<IStationRepository>();
            mockRepo.Setup(mock => mock.DeleteStation(0)).Returns(false);
            mockRepo.Setup(mock => mock.DeleteStation(1)).Returns(true);

            return mockRepo.Object;
        }
    }
}