using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication_Vy.Service.Contracts;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Service.Implementation;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new Db.VyDbContext();
            db.Database.Initialize(true);

            return View();
        }

        [HttpPost]
        public ActionResult RegisterTicket(TicketDTO ticket)
        {
            System.Diagnostics.Debug.WriteLine(ticket.CustomerDTO.Zipcode);

            return Json(new { result = "Redirect", url = Url.Action("tickets", "Home") });
        }

        [HttpGet]
        public string GetTrips()
        {
            VyServiceImpl service = new VyServiceImpl();
            List<Models.DTO.TripDTO> trips = service.GetTripDtos();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(trips);
            return json;
        }

        public ActionResult Tickets()
        {
            VyServiceImpl service = new VyServiceImpl();
            List<Models.DTO.TicketDTO> tickets = service.GetTicketDtos();
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
            IExampleService service = new ExampleServiceImpl();
             //return View(service.GetExampleEntityDto());
             throw new NotImplementedException();
        }
    }
}