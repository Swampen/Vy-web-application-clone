﻿using System.Collections.Generic;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using MODEL.Models.Entities;
using Moq;

namespace Test.MockUtil.RepositoryMock
{
    public class StationRepositoryMock
    {
        private static List<Station> _stations = new List<Station>
        {
            new Station{Id = 1, Name = "test1", StopId = "1"},
            new Station{Id = 2, Name = "test2", StopId = "2"},
            new Station{Id = 3, Name = "test3", StopId = "3"},
            new Station{Id = 4, Name = "test4", StopId = "4"}
        }; 
        public static IStationRepository FindAllStationsMock()
        {
            var mockRepo = new Mock<IStationRepository>();
            mockRepo.Setup(mock => mock.FindAllStations()).Returns(_stations);
            return mockRepo.Object;
        }
    }
}