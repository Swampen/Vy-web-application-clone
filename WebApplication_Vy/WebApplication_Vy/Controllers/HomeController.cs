﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication_Vy.Db;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITripService _tripService;
        private readonly IVyService _vyService;

        public HomeController(IVyService vyService, ITripService tripService)
        {
            _vyService = vyService;
            _tripService = tripService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }

        public ActionResult Index()
        {
            ViewBag.Stations = _tripService.GetAllStationDtos();
            return View();
        }

        
        [HttpPost]
        public ActionResult CustomerDetails(TripDTO selectedTripDto)
        {
            ViewBag.Model = selectedTripDto;
            return View();
        }

        [HttpPost]
        public ActionResult Trips(TripQuerryDTO tripQuerry)
        {
            ViewBag.Model = tripQuerry;
            return View(tripQuerry);
        }

        [HttpGet]
        public ActionResult Trips()
        {
            var tripQuerry = new TripQuerryDTO
            {
                Arrival_Station = "Bodø",
                Departure_Station = "Oslo",
                Date = "2019-09-26",
                Time = "11:09"
            };
            ViewBag.Model = tripQuerry;

            return View(tripQuerry);
        }

        [HttpPost]
        public ActionResult RegisterTicket(TicketDTO ticketDTO)
        {
            if (ModelState.IsValid)
            {
                var success = _vyService.CreateTicket(ticketDTO);
                if (success) return RedirectToAction("tickets");
            }

            return View("Index");
        }

        [HttpPost]
        public string SearchStation(string query)
        {
            Debug.WriteLine(query);
            var stations = _tripService.FindStationsMatching(query);
            var jsonSerialiser = new JavaScriptSerializer();
            return jsonSerialiser.Serialize(stations);
        }

        [HttpGet]
        public string GetStation()
        {
            var stations = _tripService.GetAllStationDtos();
            var jsonSerialiser = new JavaScriptSerializer();
            return jsonSerialiser.Serialize(stations);
        }

        [HttpPost]
        public string SearchZip(ZipcodeDTO zipcode)
        {
            var match = Regex.Match(zipcode.Postalcode, "[0-9]{4}");
            if (!match.Success) return "";
            var result = _vyService.GetPostaltown(zipcode.Postalcode);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("zip", "Not a valid Norwegian zipcode");
                return result;
            }

            return result;
        }

        public ActionResult Tickets()
        {
            var tickets = _vyService.GetTicketDtos();
            return View(tickets);
        }

        public ActionResult Contact()
        {
            ViewBag.Current = "Contact";

            ViewBag.Message = "Your contact page.";
            return View();
        }


        public ActionResult ViewAllExampleEntities()
        {
            _vyService.GetCustomerDtos();

            // The next line is commented out to avoid creating a dummy view-file.
            //return View(service.GetExampleEntityDto());
            throw new NotImplementedException();
        }
    }
}