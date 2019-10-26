using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL.Service.Contracts;
using DAL.DTO;
using DAL.DTO.TripData;
using log4net;
using UTILS.Utils.Filters;
using UTILS.Utils.Logging;

namespace WebApplication_Vy.Controllers
{
    [ControllerExceptionFilter]
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
        }

        public ActionResult Index()
        {
            if (Session["Auth"] == null)
            {
                Log.Info("setting auth to false");
                Session["Auth"] = false;
            }

            Session["HaveRoundTrip"] = false;
            Session["ChosenTrips"] = new List<TripDTO>();
            Session["Confirmed"] = false;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        Departure_StationId = tripQuery.Arrival_StationId,
                        Arrival_Station = tripQuery.Departure_Station,
                        Arrival_StationId = tripQuery.Departure_StationId,
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

            Log.Warn("Invalid modelstate detected, redirecting to index");

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
        [ValidateAntiForgeryToken]
        public ActionResult RegisterTicket(SubmitPurchaseDto submitPurchaseDto)
        {
            var confirmed = Session["Confirmed"] == null ? false : (bool) Session["Confirmed"];
            if (ModelState.IsValid)
            {
                if (confirmed) return RedirectToAction("Index");
                var success = _vyService.CreateTicket(submitPurchaseDto.TripTicket);
                if (submitPurchaseDto.ReturnTripTicket.ArrivalStation != null)
                {
                    submitPurchaseDto.ReturnTripTicket.Customer = submitPurchaseDto.TripTicket.Customer;
                    submitPurchaseDto.ReturnTripTicket.CreditCard = submitPurchaseDto.TripTicket.CreditCard;
                    success = _vyService.CreateTicket(submitPurchaseDto.ReturnTripTicket);
                }

                if (success)
                {
                    Log.Info("create ticket event succeded, redirecting to confirmation page");
                    Session["Confirmed"] = true;
                    ViewBag.trip = (List<TripDTO>) Session["ChosenTrips"];
                    return View("Confirmation");
                }
            }

            Log.Warn("invalid modelstate detecte, could not create ticket");

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

        [HttpGet]
        public string GetAllStations()
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(_stationService.getAllKeyValueStations());
        }
    }
}