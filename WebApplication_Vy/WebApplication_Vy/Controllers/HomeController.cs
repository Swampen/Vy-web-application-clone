using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using WebApplication_Vy.Db;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVyService _vyService;

        public HomeController(IVyService vyService)
        {
            _vyService = vyService;
        }

        public ActionResult Index()
        {
            var db = new VyDbContext();
            db.Database.Initialize(true);

            return View();
        }

        [HttpPost]
        public ActionResult RegisterTicket()
        {
            return RedirectToAction("Tickets");
        }

        [HttpGet]
        public string GetTrips()
        {
            var trips = _vyService.GetTripDtos();
            
            
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(trips);
            return json;
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