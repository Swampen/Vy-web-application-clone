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
        private readonly ILoginService _loginService;

        public AdminController(
            IVyService vyService,
            IStationService stationService,
            ILoginService loginService
        )
        {
            _vyService = vyService;
            _stationService = stationService;
            _loginService = loginService;

            var db = new VyDbContext();
            db.Database.Initialize(true);
        }
        // GET: Admin
        public ActionResult Index()
        {
            var session = (bool)Session["Auth"];
            if (session)
            {
                return View();
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Tickets()
        {
            var session = (bool)Session["Auth"];
            if (session)
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

        [ValidateAntiForgeryToken]
        public ActionResult DeleteTicket(int ticketId)
        {
            var session = (bool)Session["Auth"];
            if (session)
            {
                var success = _vyService.DeleteTicket(ticketId);
                return RedirectToAction("Tickets");
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Stations()
        {
            var stations = _stationService.getAllStations();
            return View(stations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStation(StationDTO station)
        {
            var success = _vyService.ChangeStation(station);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Customers()
        {
            Session["SuperUser"] = true;
            var customers = _vyService.GetCustomerDtos();
            return View(customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(CustomerDto c)
        {

            if (ModelState.IsValid)
            {
                if (_vyService.UpdateCustomer(c))
                {
                    return RedirectToAction("Customers");
                }
            }
            var customers = _vyService.GetCustomerDtos();
            return View("Customers", customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int customerId)
        {
            _vyService.DeleteCustomer(customerId);
            return RedirectToAction("Customers");
        }

        public ActionResult Logout()
        {
            Session["Auth"] = false;
            return RedirectToAction("index", "home");
        }


        public ActionResult Admins()
        {
            var admins = _loginService.GetAllAdmins();
            return View(admins);
        }

        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewAdmin(AdminUserDTO adminUserDto)
        {
            try
            {
                var superUser = (bool)Session["SuperAdmin"];
                if (superUser)
                {
                    var UserCreated = _loginService.RegisterAdminUser(adminUserDto.Username,
                        adminUserDto.Password, "ADMINISTRATOR");
                    if (UserCreated)
                    {
                        return RedirectToAction("admins");
                    }
                }
                return RedirectToAction("admins");
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }
    }
}