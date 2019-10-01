using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
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
        private readonly IZipSearchService _zipSearchService;

        public HomeController(
            IVyService vyService,
            ITripService tripService,
            IZipSearchService zipSearchService)
        {
            _vyService = vyService;
            _tripService = tripService;
            _zipSearchService = zipSearchService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }

        public ActionResult Index()
        {
            //TODO: This can probably be deleted
            //ViewBag.Stations = _tripService.GetAllStationDtos();
            return View();
        }

        [HttpPost]
        public ActionResult Index(TripQueryDTO tripQuery)
        {
            Session["ChosenTrips"] = new List<TripDTO>();
            if (tripQuery.Round_Trip)
            {
                var returnTripQuery = new TripQueryDTO(){
                    Departure_Station = tripQuery.Arrival_Station,
                    Arrival_Station = tripQuery.Departure_Station,
                    Date = tripQuery.Return_Date,
                    Time = tripQuery.Return_Time,
                    Round_Trip = false,
                };
                Session["ReturnTripQuery"] = returnTripQuery;
            }
            ViewBag.Model = tripQuery;

            return View("Trips");
        }

        

        [HttpPost]
        public ActionResult Trips(TripDTO selectedTripDto)
        {
            var chosenTrips = (List<TripDTO>)Session["ChosenTrips"];
            chosenTrips.Add(selectedTripDto);
            if (selectedTripDto.Round_Trip)
            {   
                var returnQuery = (TripQueryDTO)Session["ReturnTripQuery"];
                ViewBag.Model = returnQuery;
                return View();
            }
            ViewBag.Model = chosenTrips;
            Console.WriteLine("All is good!!!!");
            return View("CustomerDetails");
        }
        
        //[HttpPost]
        //public ActionResult Trips(TripDTO selectedTripDto)
        //{
        //    
        //    ViewBag.Model = selectedTripDto;
        //    return View("CustomerDetails");
        //}

        [HttpGet]
        public ActionResult Trips(TripQueryDTO tripQuerry)
        {
            Session["ChosenTrips"] = new List<TripDTO>();
            return View();
        }
        
        [HttpPost]
        public ActionResult RegisterTicket(SubmitPurchaseDTO submitPurchaseDto)
        {
            Console.WriteLine(submitPurchaseDto.TripTicket.DepartureStation);
            if (ModelState.IsValid)
            {
                var success = _vyService.CreateTicket(submitPurchaseDto);
                if (success) return RedirectToAction("tickets");
            }

            return View("Index");
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
            /*CustomerDTO customer = new CustomerDTO()
            {
                Givenname = "Nils",
                Surname = "Nilsen",
                Address = "SAd asddswww 89",
                Zipcode = new ZipcodeDTO()
                {
                    Postalcode = "0659",
                    Postaltown = "Oslo"
                },
                
                Tickets = new List<TicketDTO>(),
            };

            customer.Tickets.Add(new TicketDTO()
            {
                DepartureStation = "Oslo",
                ArrivalStation = "Bodø",
                DepartureTime = "19:20",
                ArrivalTime = "09:19",
                Price = 1950,
                Duration = "1d0h15m",
                TrainChanges = "1",
            });

            customer.Tickets.Add(new TicketDTO()
            {
                DepartureStation = "Oslo",
                ArrivalStation = "Bodø",
                DepartureTime = "19:20",
                ArrivalTime = "09:19",
                Price = 1950,
                Duration = "1d0h15m",
                TrainChanges = "1",
            });

            CustomerDTO customer2 = new CustomerDTO()
            {
                Givenname = "Hans",
                Surname = "Hansen",
                Address = "Bygdøy Alle 89",
                Zipcode = new ZipcodeDTO()
                {
                    Postalcode = "0262",
                    Postaltown = "Oslo"
                },

                Tickets = new List<TicketDTO>(),
            };

            customer2.Tickets.Add(new TicketDTO()
            {
                DepartureStation = "Oslo",
                ArrivalStation = "Bodø",
                DepartureTime = "23:20",
                ArrivalTime = "13:19",
                Price = 1950,
                Duration = "0d12h01m",
                TrainChanges = "1",
            });

            var customers = new List<CustomerDTO>();
            customers.Add(customer);
            customers.Add(customer2);*/

            List<CustomerDTO> customers = _vyService.GetCustomerDtos();
            //_vyService.GetTicketDtos()
            return View(customers);
        }
    }
}