﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;

namespace WebApplication_Vy.Models.Profiles
{
    public class CusToCusDTOProfile : Profile
    {
        public CusToCusDTOProfile()
        {
            CreateMap<Customer, CustomerDTO>();
        }
    }

    public class TicToTicDTOProfile : Profile
    {
        public TicToTicDTOProfile()
        {
            CreateMap<Ticket, TicketDTO>();
        }
    }

    public class TriToTriDTOProfile : Profile
    {
        public TriToTriDTOProfile()
        {
            CreateMap<Trip, TripDTO>();
        }
    }

    public class ZipToZipDTOProfile : Profile
    {
        public ZipToZipDTOProfile()
        {
            CreateMap<Zipcode, ZipcodeDTO>();
        }
    }


}