using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BLL.Service.Contracts;
using DAL.Db;
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
        private readonly ILoginService _loginService;

        public HomeController(
            IVyService vyService,
            IZipSearchService zipSearchService,
            IStationService stationService,
            ILoginService loginService
        )
        {
            _vyService = vyService;
            _zipSearchService = zipSearchService;
            _stationService = stationService;
            _loginService = loginService;

            
        }
        
        public ActionResult Index()
        {
            if(Session["Auth"] == null)
            {
                Session["Auth"] = false;
            }
            Log.Info("Application started, log4net running.....");
            Session["HaveRoundTrip"] = false;
            Session["ChosenTrips"] = new List<TripDTO>();
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

                if (success) return RedirectToAction("Index");
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

        [HttpGet]
        public string GetAllStations()
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(_stationService.getAllKeyValueStations());
        }
        
        [HttpPost]
        public ActionResult Login(AdminUserDTO adminUserDTO)
        {
            var login = _loginService.Login(adminUserDTO);
            Console.WriteLine(login);
            if (login)
            {
                Session["Auth"] = true;
                Console.WriteLine("login succeeded");
                if (adminUserDTO.SuperAdmin)
                {
                    Session["SuperAdmin"] = true;
                }
                
                return RedirectToAction("index", "admin");
            }
            else
            {
                ModelState.AddModelError("", "Wrong username or password");
                Session["Auth"] = false;
                Session["SuperAdmin"] = false;
                Console.WriteLine("login failed");
                return RedirectToAction("index");
            }
        }
//        [HttpPost]
//        public ActionResult Registrer(string Username, string Password, string SecretAdminPassword)
//        {
//            
//            if (_loginService.RegisterAdminUser(Username, Password, SecretAdminPassword))
//            {
//                Session["Auth"] = true;
//                return Redirect("http://localhost:5000/admin");
//            }
//            else
//            {
//                Session["Auth"] = false;
//                return RedirectToAction("Index");
//            }
//        }
    }
}