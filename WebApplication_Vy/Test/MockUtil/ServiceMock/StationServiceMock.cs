using System.Collections.Generic;
using BLL.Service.Contracts;
using Moq;

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
    }
}