using System;
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
        public ActionResult Index(TripQuerryDTO tripQuerry)
        {
            //TODO: this can probably be deleted
            //ViewBag.Stations = _tripService.GetAllStationDtos();
            return RedirectToAction("Trips", tripQuerry);
        }


        [HttpPost]
        public ActionResult CustomerDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Trips(TripDTO selectedTripDto)
        {
            ViewBag.Model = selectedTripDto;
            return View("CustomerDetails");
        }

        [HttpGet]
        public ActionResult Trips(TripQuerryDTO tripQuerry)
        {
            ViewBag.Model = tripQuerry;
            return View();
        }
        
        [HttpPost]
        public ActionResult RegisterTicket(SubmitPurchaseDTO submitPurchaseDto)
        {
            Console.WriteLine(submitPurchaseDto.Ticket.DepartureStation);
            if (ModelState.IsValid)
            {
                var success = _vyService.CreateTicket(submitPurchaseDto);
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
            return View(_vyService.GetTicketDtos());
        }

        public ActionResult Contact()
        {
            ViewBag.Current = "Contact";

            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}