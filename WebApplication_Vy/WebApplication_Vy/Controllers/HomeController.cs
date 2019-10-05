using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using WebApplication_Vy.Db;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.DTO.TripData;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ITripService _tripService;
        private readonly IVyService _vyService;
        private readonly IZipSearchService _zipSearchService;

        public HomeController(
            IVyService vyService,
            ITripService tripService,
            IZipSearchService zipSearchService,
            ICreditCardService creditCardService)
        {
            _vyService = vyService;
            _tripService = tripService;
            _zipSearchService = zipSearchService;
            _creditCardService = creditCardService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }

        public ActionResult Index()
        {
            Session["HaveRoundTrip"] = false;
            Session["ChosenTrips"] = new List<TripDTO>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(TripQueryDTO tripQuery)
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
        public ActionResult RegisterTicket(SubmitPurchaseDTO submitPurchaseDto)
        {
            if (ModelState.IsValid)
            {
                submitPurchaseDto
                    .TripTicket
                    .Customer
                    .CreditCards = new List<CardDTO>{submitPurchaseDto.CreditCard};

                var success = _vyService.CreateTicket(submitPurchaseDto.TripTicket);
                if (submitPurchaseDto.ReturnTripTicket.ArrivalStation != null)
                {
                    submitPurchaseDto.ReturnTripTicket.Customer = submitPurchaseDto.TripTicket.Customer;
                    success = _vyService.CreateTicket(submitPurchaseDto.ReturnTripTicket);
                    Console.WriteLine("Returnticket success");
                }

                if (success) return RedirectToAction("tickets");
            }

            return View("tickets");
        }

        [HttpGet]
        public ActionResult GetPaymentDetails(int id)
        {
            ViewBag.Model = _creditCardService.GetCreditCard(id);
            return null;
        }

        [HttpGet]
        public ActionResult Card()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Card(CardDTO creditCardDTO)
        {
            return View();
        }

        [HttpPost]
        public string SearchZip(ZipcodeDTO zipcode)
        {
            var match = Regex.Match(zipcode.Postalcode, "[0-9]{4}");
            if (!match.Success) return "";
            var result = _zipSearchService.GetPostaltown(zipcode.Postalcode);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("zip", "Not a valid Norwegian zipcode");
                return result;
            }

            return result;
        }

        public ActionResult Tickets()
        {
            var customers = _vyService.GetCustomerDtos();
            return View(customers);
        }
    }
}