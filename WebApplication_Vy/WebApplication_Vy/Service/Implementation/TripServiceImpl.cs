using System.Collections.Generic;
using AutoMapper;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.DTO;
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
    }
}