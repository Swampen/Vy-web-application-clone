﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL.Service.Contracts;
using DAL.Db;
using DAL.DTO;
using DAL.DTO.TripData;
using log4net;
using WebApplication_Vy.Utils.Logging;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        private readonly IStationService _stationService;

        private readonly IVyService _vyService;
        private readonly IZipSearchService _zipSearchService;

        public HomeController(
            IVyService vyService,
            IZipSearchService zipSearchService,
            IStationService stationService
        )
        {
            _vyService = vyService;
            _zipSearchService = zipSearchService;
            _stationService = stationService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }

        public ActionResult Index()
        {
            Log.Info("Application started, log4net running.....");
            Session["HaveRoundTrip"] = false;
            Session["ChosenTrips"] = new List<TripDTO>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(TripQueryDTO tripQuery)
        {
            if (ModelState.IsValid)
            {
                Session["HaveRoundTrip"] = tripQuery.Round_Trip;
                if (tripQuery.Round_Trip)
                {
                    var returnTripQuery = new TripQueryDTO
                    {
                        Departure_Station = tripQuery.Arrival_Station,
                        Arrival_Station = tripQuery.Departure_Station,
                        Date = tripQuery.Return_Date,
                        Time = tripQuery.Return_Time,
                        Round_Trip = false,
                        Adult = tripQuery.Adult,
                        Child = tripQuery.Child,
                        Student = tripQuery.Student,
                        Senior = tripQuery.Senior
                    };
                    Session["ReturnTripQuery"] = returnTripQuery;
                }

                ViewBag.Model = tripQuery;

                return View("Trips");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Trips(TripDTO selectedTripDto)
        {
            var haveRoundTrip = (bool) Session["HaveRoundTrip"];
            if (selectedTripDto.Round_Trip)
            {
                Session["ToTrip"] = selectedTripDto;
                var returnQuery = (TripQueryDTO) Session["ReturnTripQuery"];
                ViewBag.Model = returnQuery;
                return View();
            }

            var chosenTrips = new List<TripDTO>();
            if (haveRoundTrip)
            {
                var toTrip = (TripDTO) Session["ToTrip"];
                chosenTrips.Add(toTrip);
            }

            chosenTrips.Add(selectedTripDto);
            Session["ChosenTrips"] = chosenTrips;
            ViewBag.Model = chosenTrips;
            return View("CustomerDetails");
        }

        [HttpGet]
        public ActionResult Trips(TripQueryDTO tripQuerry)
        {
            Session["ChosenTrips"] = new List<TripDTO>();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterTicket(SubmitPurchaseDto submitPurchaseDto)
        {
            if (ModelState.IsValid)
            {
                var success = _vyService.CreateTicket(submitPurchaseDto.TripTicket);
                if (submitPurchaseDto.ReturnTripTicket.ArrivalStation != null)
                {
                    submitPurchaseDto.ReturnTripTicket.Customer = submitPurchaseDto.TripTicket.Customer;
                    submitPurchaseDto.ReturnTripTicket.CreditCard = submitPurchaseDto.TripTicket.CreditCard;
                    success = _vyService.CreateTicket(submitPurchaseDto.ReturnTripTicket);
                    Console.WriteLine("Returnticket success");
                }

                if (success) return RedirectToAction("tickets");
            }

            var chosenTrips = (List<TripDTO>) Session["ChosenTrips"];
            ViewBag.Model = chosenTrips;
            return View("CustomerDetails");
        }

        [HttpPost]
        public string SearchZip(ZipcodeDto zipcode)
        {
            var match = Regex.Match(zipcode.Postalcode, "[0-9]{4}");
            if (!match.Success) return "";
            var result = _zipSearchService.GetPostaltown(zipcode.Postalcode);
            return result;
        }

        public ActionResult Tickets()
        {
            var customers = _vyService.GetCustomerDtos();
            customers.ForEach(dto =>
            {
                dto.Tickets.ForEach(ticketDto => { _vyService.MaskCreditCardNumber(ticketDto.CreditCard); });
            });
            return View(customers);
        }

        [HttpDelete]
        public ActionResult DeleteTicket(int ticketId)
        {
            var success = _vyService.DeleteTicket(ticketId);
            return RedirectToAction("Tickets");
        }

        [HttpGet]
        public string GetAllStations()
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(_stationService.getAllKeyValueStations());
        }
    }
}