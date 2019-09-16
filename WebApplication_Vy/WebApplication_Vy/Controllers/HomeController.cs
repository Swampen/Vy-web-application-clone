using System;
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
            System.Diagnostics.Debug.WriteLine(ticketDTO.CustomerDTO.ZipcodeDTO.Postalcode);
            bool success = _vyService.CreateTicket(ticketDTO);
            return Json(new { result = success.ToString() , url = Url.Action("tickets", "Home") });
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