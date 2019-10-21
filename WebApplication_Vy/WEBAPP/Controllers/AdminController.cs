using BLL.Service.Contracts;
using DAL.Db;
using DAL.DTO;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTILS.Utils.Logging;

namespace WebApplication_Vy.Controllers
{
    public class AdminController : Controller
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        private readonly IStationService _stationService;

        private readonly IVyService _vyService;
        private readonly IZipSearchService _zipSearchService;

        public AdminController(
            IVyService vyService,
            IZipSearchService zipSearchService,
            IStationService stationService
        )
        {
            _vyService = vyService;
            _zipSearchService = zipSearchService;
            _stationService = stationService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tickets()
        {
            var customers = _vyService.GetCustomerDtos();
            customers.ForEach(dto =>
            {
                dto.Tickets.ForEach(ticketDto => { _vyService.MaskCreditCardNumber(ticketDto.CreditCard); });
            });
            return View(customers);
        }

        [HttpDelete]
        public ActionResult DeleteTicket(int ticketId)
        {
            var success = _vyService.DeleteTicket(ticketId);
            return RedirectToAction("Tickets");
        }

        public ActionResult Stations()
        {
            //sjekker om du er logget inn som admin og hvis du er det redirecter den til AdminPage
            //Foreløpig kjøres det bare en refresh hvis du ikke er logget inn.
            /*bool valid = (bool)Session["AdminLogin"];
            if (valid)
            {
                var stations = _stationService.getAllStations();
                return View(stations);
            }
            else
            {
                return Redirect(Request.UrlReferrer.ToString());
            }*/
            var stations = _stationService.getAllStations();
            return View(stations);
        }



        [HttpPost]
        public ActionResult EditStation(StationDTO station)
        {
            var success = _vyService.ChangeStation(station);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}