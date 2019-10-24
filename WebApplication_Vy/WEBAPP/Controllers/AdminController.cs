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
        private readonly ILoginService _loginService;
        private readonly IVyService _vyService;

        public AdminController(
            IVyService vyService,
            IStationService stationService,
            ILoginService loginService
        )
        {
            _loginService = loginService;
            _vyService = vyService;
            _stationService = stationService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                return View();
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Tickets()
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                var customers = _vyService.GetCustomerDtos();
                customers.ForEach(dto =>
                {
                    dto.Tickets.ForEach(ticketDto => { _vyService.MaskCreditCardNumber(ticketDto.CreditCard); });
                });
                return View(customers);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteTicket(int ticketId)
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                var success = _vyService.DeleteTicket(ticketId);
                return RedirectToAction("Tickets");
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Stations()
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                var stations = _stationService.getAllStations();
                return View(stations);
            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStation(StationDTO station)
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                var success = _vyService.ChangeStation(station);
                return RedirectToAction("stations");
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Customers()
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                var customers = _vyService.GetCustomerDtos();
                return View(customers);
            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(CustomerDto c)
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                if (ModelState.IsValid)
                {
                    if (_vyService.UpdateCustomer(c))
                    {
                        return RedirectToAction("Customers");
                    }
                }
                return RedirectToAction("Customers");
            }
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                _vyService.DeleteCustomer(customerId);
                return RedirectToAction("Customers");
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Admins()
        {
            if (Session["Auth"] != null && (bool)Session["Auth"])
            {
                var admins = _loginService.GetAllAdmins();
                return View(admins);
            }
            return RedirectToAction("index", "home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewAdmin(AdminUserDTO adminUserDto)
        {

            if (Session["SuperAdmin"] != null && (bool)Session["SuperAdmin"])
            {
                
                var UserCreated = _loginService.RegisterAdminUser(adminUserDto.Username,
                    adminUserDto.Password, "ADMINISTRATOR");
                if (UserCreated)
                {
                    return RedirectToAction("admins");
                }
                TempData["error"] = "Admin already exists";
            }
            return RedirectToAction("admins");
        }

        public ActionResult DeleteAdmin(int adminId)
        {
            if (Session["SuperAdmin"] != null && (bool)Session["SuperAdmin"])
            {
                var success = _loginService.DeleteAdmin(adminId);
            }
            return RedirectToAction("admins");
        }
    }
}