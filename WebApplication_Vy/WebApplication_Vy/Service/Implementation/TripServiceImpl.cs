using System.Collections.Generic;
using AutoMapper;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Service.Implementation
{
    public class TripServiceImpl : ITripService
    {
        private ITripRepository _tripRepository;
        
        public TripServiceImpl(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public List<TripDTO> GetAllTripDtos()
        {
            List<Trip> trips = _tripRepository.FindAllTrips();
            var dtos = new List<TripDTO>();
            foreach (var trip in trips)
            {
                dtos.Add(MapTripDTO(trip));
            }
            
            return dtos;
        }

        public List<TripDTO> FindTripsMatching(string query)
        {
            List<Trip> trips = _tripRepository.TripSearch(query);
            var dtos = new List<TripDTO>();
            foreach (var trip in trips)
            {
                dtos.Add(MapTripDTO(trip));
            }

            return dtos;
        }

        private TripDTO MapTripDTO(Trip trip)
        {
            var config = new MapperConfiguration(cgf => cgf
                .CreateMap<Trip, TripDTO>()
                .ReverseMap());
            var mapper = config.CreateMapper();
            return mapper.Map<TripDTO>(trip);
        }

        public List<StationDTO> FindStationsMatching(string query)
        {
            List<Station> all = _tripRepository.FindAllStations();
            var dtos = new List<StationDTO>();
            foreach (var station in all)
            {
                if (station.Name.Contains(query))
                    dtos.Add(MapStationDTO(station));
            }

            return dtos;
        }

        private StationDTO MapStationDTO(Station station)
        {
            var config = new MapperConfiguration(cgf => cgf
                .CreateMap<Station, StationDTO>()
                .ReverseMap());
            var mapper = config.CreateMapper();
            return mapper.Map<StationDTO>(station);
        }

        public List<TripDTO> GetTripDtos()
        {
            List<Trip> entities = _tripRepository.FindAllTrips();
            List<TripDTO> dtos = new List<TripDTO>();
            foreach (Trip entity in entities)
            {
                dtos.Add(MapTripDto(entity));
            }

            return dtos;
        }

        private TripDTO MapTripDto(Trip entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Trip, TripDTO>().ReverseMap());
            var mapper = config.CreateMapper();
            TripDTO dto = mapper.Map<TripDTO>(entity);
            return dto;
        }

    }
}