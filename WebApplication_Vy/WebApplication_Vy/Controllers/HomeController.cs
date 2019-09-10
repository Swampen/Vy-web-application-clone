using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        }

        public ActionResult Index()
        {
            var db = new Db.VyDbContext();
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
            List<Models.DTO.TripDTO> trips = _vyService.GetTripDtos();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(trips);
            return json;
        }

        public ActionResult Tickets()
        {
            List<Models.DTO.TicketDTO> tickets = _vyService.GetTicketDtos();
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