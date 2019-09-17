using System.Collections.Generic;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Service.Implementation
{
    public class TripServiceImpl : ITripService
    {

        public TripServiceImpl()
        {
            
        }
        
        public List<TripDTO> GetAllTripDtos()
        {
            throw new System.NotImplementedException();
        }

        public List<TripDTO> FindTripsMatching(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}