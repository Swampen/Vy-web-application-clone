using System.Collections.Generic;
using BLL.Service.Contracts;
using DAL.DTO;
using Moq;

namespace Test.MockUtil.ServiceMock
{
    public class StationServiceMock
    {
        private static readonly Dictionary<string, string> StationsDictionary = new Dictionary<string, string>
        {
            ["test"] = "test"
        };

        private static readonly List<StationDTO> StationDtos = new List<StationDTO>
        {
            new StationDTO {Id = 1, Name = "test1", StopId = "test1"},
            new StationDTO {Id = 2, Name = "test2", StopId = "test2"},
            new StationDTO {Id = 3, Name = "test3", StopId = "test3"},
            new StationDTO {Id = 4, Name = "test4", StopId = "test4"}
        };

        public static IStationService GetAllKeyValueStations()
        {
            var mockservice = new Mock<IStationService>();
            mockservice.Setup(mock => mock.getAllKeyValueStations()).Returns(StationsDictionary);
            return mockservice.Object;
        }

        public static IStationService GetAllStations()
        {
            var mockService = new Mock<IStationService>();
            mockService.Setup(mock => mock.getAllStations()).Returns(StationDtos);
            return mockService.Object;
        }


        public static IStationService ChangeStationMock()
        {
            var mockService = new Mock<IStationService>();
            mockService.Setup(mock => mock.updateStation(It.IsAny<StationDTO>())).Returns(true);
            return mockService.Object;
        }

        public static IStationService DeleteStationMock(bool returnValue)
        {
            var mockService = new Mock<IStationService>();
            if (returnValue)
                mockService.Setup(mock => mock.deleteStation(It.IsAny<int>())).Returns(true);
            else
                mockService.Setup(mock => mock.deleteStation(It.IsAny<int>())).Returns(false);

            return mockService.Object;
        }

        public static IStationService CreateStationMock(bool returnValue)
        {
            var mockService = new Mock<IStationService>();
            if (returnValue)
                mockService.Setup(mock => mock.createStation(It.IsAny<StationDTO>())).Returns(true);
            else
                mockService.Setup(mock => mock.createStation(It.IsAny<StationDTO>())).Returns(false);

            return mockService.Object;
        }

        public static StationDTO StationDTOMock()
        {
            return new StationDTO
            {
                Id = 1,
                Name = "Alna Stasjon",
                StopId = "NSR:StopPlace:418"
            };
        }
    }
}