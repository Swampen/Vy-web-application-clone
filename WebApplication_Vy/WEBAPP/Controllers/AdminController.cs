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
        private readonly ICustomerService _customerService;
        private readonly IVyService _vyService;

        public AdminController(
            IVyService vyService,
            IStationService stationService,
            ICustomerService customerService
        )
        {
            _vyService = vyService;
            _stationService = stationService;
            _customerService = customerService;

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

        public ActionResult DeleteTicket(int ticketId)
        {
            var success = _vyService.DeleteTicket(ticketId);
            return RedirectToAction("Tickets");
        }

        public ActionResult Stations()
        {
            var stations = _stationService.getAllStations();
            return View(stations);
        }

        [HttpPost]
        public ActionResult UpdateStation(StationDTO station)
        {
            var success = _vyService.ChangeStation(station);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Customers()
        {
            var customers = _customerService.getAllCustomerDtos();
            ViewBag.updatedCustomer = false;
            return View(customers);
        }

        [HttpPost]
        public ActionResult UpdateCustomer(CustomerDto c)
        {
            var customers = new List<CustomerDto>();
            if (ModelState.IsValid)
            {
                if (_customerService.updateCustomer(c))
                {
                    customers = _customerService.getAllCustomerDtos();
                    return View("Customers", customers);
                }
            }
            customers = _customerService.getAllCustomerDtos();
            return View("Customers", customers);
        }

        public ActionResult DeleteCustomer(int id)
        {
            var customers = _customerService.getAllCustomerDtos();
            return View(customers);
        }
    }
}