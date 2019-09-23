using System.Collections.Generic;
using WebApplication_Vy.Models.DTO.TripData;

namespace WebApplication_Vy.Service.Contracts
{
    public interface ITripService
    {
        List<TripDTO> GetAllTripDtos();
        List<TripDTO> FindTripsMatching(string query);
        List<TripDTO> GetTripDtos();
    }
}