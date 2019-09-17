using System.Collections.Generic;
using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface ITripService
    {
        List<TripDTO> GetAllTripDtos();
        List<TripDTO> FindTripsMatching(string query);
    }
}