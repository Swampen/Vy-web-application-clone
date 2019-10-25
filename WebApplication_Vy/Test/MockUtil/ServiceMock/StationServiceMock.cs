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
            new StationDTO{Id = 1, Name = "test1", StopId = "test1"},
            new StationDTO{Id = 2, Name = "test2", StopId = "test2"},
            new StationDTO{Id = 3, Name = "test3", StopId = "test3"},
            new StationDTO{Id = 4, Name = "test4", StopId = "test4"},
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
    }
}