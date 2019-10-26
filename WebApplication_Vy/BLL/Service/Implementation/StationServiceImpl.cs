using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;
using System.Collections.Generic;

namespace BLL.Service.Implementation
{
    public class StationServiceImpl : IStationService
    {
        private readonly IStationRepository _stationRepository;

        public StationServiceImpl(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public bool createStation(StationDTO stationDto)
        {
            return _stationRepository.CreateStation(MapStationEntity(stationDto));
        }

        public bool deleteStation(int stationId)
        {
            return _stationRepository.DeleteStation(stationId);
        }

        public Dictionary<string, string> getAllKeyValueStations()
        {
            Dictionary<string, string> stations = new Dictionary<string, string>();
            foreach (var station in _stationRepository.FindAllStations())
            {
                stations.Add(station.Name, station.StopId);
            }
            return stations;
        }

        public List<StationDTO> getAllStations()
        {
            var stationDtos = new List<StationDTO>();
            _stationRepository
                .FindAllStations()
                .ForEach(station => { stationDtos.Add(MapStationDto(station)); });
            return stationDtos;
        }

        public bool updateStation(StationDTO stationDto)
        {
            return _stationRepository.UpdateStation(MapStationEntity(stationDto));
        }

        private StationDTO MapStationDto(Station station)
        {
            return new StationDTO
            {
                Id = station.Id,
                Name = station.Name,
                StopId = station.StopId
            };
        }

        private Station MapStationEntity(StationDTO dto)
        {
            return new Station
            {
                Id = dto.Id,
                Name = dto.Name,
                StopId = dto.StopId
            };
        }
    }
}