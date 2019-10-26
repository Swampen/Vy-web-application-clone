using BLL.Service.Implementation;
using DAL.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using Test.MockUtil.RepositoryMock;
using Test.MockUtil.ServiceMock;

namespace Test.Service
{
    public class StationServiceImplTest
    {
        private StationServiceImpl _service;
        private StationDTO _stationDto;


        [Test]
        public void GetAllKeyValueStations_shouldReturnDictionary()
        {
            //Arrange
            var service = new StationServiceImpl(StationRepositoryMock.FindAllStationsMock());

            //Act
            var actual = service.getAllKeyValueStations();

            //Assert
            Assert.IsInstanceOf<Dictionary<string, string>>(actual);
            Assert.AreEqual(4, actual.Count);
            foreach (var station in actual)
            {
                Assert.AreEqual(station.Key, station.Value);
            }
        }

        [Test]
        public void getAllStations_shouldReturnList()
        {
            //Arrange
            var service = new StationServiceImpl(StationRepositoryMock.FindAllStationsMock());

            //Act
            var actual = service.getAllStations();

            //Assert
            Assert.IsInstanceOf<List<StationDTO>>(actual);
            Assert.AreEqual(4, actual.Count);
            foreach (var station in actual)
            {
                Assert.IsInstanceOf<StationDTO>(station);
                Assert.IsNotNull(station.Id);
                Assert.IsNotNull(station.StopId);
                Assert.IsNotNull(station.Name);
            }
        }

        [Test]
        public void ChangeStation_shouldReturnTrue()
        {
            //Arrange
            _service = new StationServiceImpl(StationRepositoryMock.UpdateStationMock());
            _stationDto = StationServiceMock.StationDTOMock();
            //Act
            var actual = _service.updateStation(_stationDto);

            //Assert
            Assert.IsTrue(actual);
        }
    }
}