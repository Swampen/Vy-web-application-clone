using System.Collections.Generic;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;

namespace BLL.Service.Implementation
{
    public class StationServiceImpl : IStationService
    {
        private readonly IStationRepository _stationRepository;

        public StationServiceImpl(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }
        
        public List<StationDTO> getAllStations()
        {
            List<StationDTO> stationDtos = new List<StationDTO>();
            _stationRepository
                .FindAllStations()
                .ForEach(station => { stationDtos.Add(MapStationDto(station)); });
            return stationDtos;
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
    }
}