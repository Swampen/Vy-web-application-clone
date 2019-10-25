using BLL.Service.Contracts;
using DAL.DTO;
using Moq;
using System.Collections.Generic;

namespace Test.MockUtil.ServiceMock
{
    public class StationServiceMock
    {
        public static readonly Dictionary<string, string> stations = new Dictionary<string, string>
        {
            ["test"] = "test"
        };

        public static IStationService GetAllKeyValueStations()
        {
            var mockservice = new Mock<IStationService>();
            mockservice.Setup(mock => mock.getAllKeyValueStations()).Returns(stations);
            return mockservice.Object;
        }

        public static StationDTO StationDTOMock()
        {
            return new StationDTO
            {
                Id = 1,
                Name = "Alna Stasjon",
                StopId = "NSR:StopPlace:418",
            };
        }
    }
}