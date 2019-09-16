using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using WebApplication_Vy.Db;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Service.Contracts;
using WebApplication_Vy.Service.Implementation;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVyService _vyService;

        public HomeController(IVyService vyService)
        {
            _vyService = vyService;
            var db = new VyDbContext();
            db.Database.Initialize(true);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterTicket(TicketDTO ticketDTO)
        {
            if (ModelState.IsValid){ 
            bool success = _vyService.CreateTicket(ticketDTO);
                if (success){ 
                    return RedirectToAction("tickets");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public string GetTrips()
        {
            var trips = _vyService.GetTripDtos();
            
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(trips);
            return json;
        }

        [HttpPost]
        public string SearchZip(ZipcodeDTO zipcode)
        {
            Match match = Regex.Match(zipcode.Postalcode, "[0-9]{4}");
            if (!match.Success) {
                return "";
            }
            var result = _vyService.GetPostaltown(zipcode.Postalcode);

            if (!ModelState.IsValid) {
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

        [HttpPost]
        public ActionResult MakeCustomer(string json)
        {
            if (json != null)
            {
                return Json("Success");
            }
            return Json("Failure");
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