using System.Web.Mvc;
using BLL.Service.Contracts;
using DAL.DTO;
using log4net;
using UTILS.Utils.Logging;

namespace WebApplication_Vy.Controllers
{
    public class AdminController : Controller
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        private readonly ILoginService _loginService;
        private readonly IStationService _stationService;
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
        }

        public ActionResult Tickets()
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                var customers = _vyService.GetCustomerDtos();
                customers.ForEach(dto =>
                {
                    dto.Tickets.ForEach(ticketDto => { _vyService.MaskCreditCardNumber(ticketDto.CreditCard); });
                });
                return View(customers);
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: view all customers");
            return RedirectToAction("index", "Home");
        }

        public ActionResult DeleteTicket(int ticketId)
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                _vyService.DeleteTicket(ticketId);
                return RedirectToAction("Tickets");
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: delete ticket");
            return RedirectToAction("index", "home");
        }

        [Route("admin/stations")]
        [Route("admin")]
        public ActionResult Stations()
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                var stations = _stationService.getAllStations();
                return View(stations);
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: view all stations");
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStation(StationDTO station)
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                _stationService.updateStation(station);
                return RedirectToAction("stations");
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: update station");
            return RedirectToAction("index", "home");
        }

        public ActionResult DeleteStation(int stationId)
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                _stationService.deleteStation(stationId);
                return RedirectToAction("stations");
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: delete station");
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult CreateStation(StationDTO stationDto)
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                _stationService.createStation(stationDto);
                return RedirectToAction("stations");
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: create station");
            return RedirectToAction("index", "home");
        }

        public ActionResult Customers()
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                var customers = _vyService.GetCustomerDtos();
                return View(customers);
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: view all customers");
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(CustomerDto c)
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                if (ModelState.IsValid)
                    if (_vyService.UpdateCustomer(c))
                        return RedirectToAction("Customers");
                Log.Warn("Update customer event failed due to invalid modelstate");
                return RedirectToAction("Customers");
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: update customer");
            return RedirectToAction("index", "home");
        }

        public ActionResult DeleteCustomer(int customerId)
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                _vyService.DeleteCustomer(customerId);
                return RedirectToAction("Customers");
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: delete customers");
            return RedirectToAction("Index", "home");
        }

        public ActionResult Admins()
        {
            if (Session["Auth"] != null && (bool) Session["Auth"])
            {
                var admins = _loginService.GetAllAdmins();
                return View(admins);
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: view all admins");
            return RedirectToAction("index", "home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewAdmin(AdminUserDTO adminUserDto)
        {
            if (Session["SuperAdmin"] != null && (bool) Session["SuperAdmin"])
            {
                var userCreated = _loginService.RegisterAdminUser(adminUserDto.Username,
                    adminUserDto.Password);
                if (userCreated) return RedirectToAction("admins");
                TempData["error"] = "Admin already exists";
            }

            Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                     ": user is not authorized to perform action: register new admin");
            return RedirectToAction("admins");
        }

        public ActionResult DeleteAdmin(int adminId)
        {
            if (Session["SuperAdmin"] != null && (bool) Session["SuperAdmin"])
                _loginService.DeleteAdmin(adminId);
            else
                Log.Warn(LogEventPrefixes.AUTHORIZATION_ERROR +
                         ": user is not authorized to perform action: delete admin");

            return RedirectToAction("admins");
        }
    }
}