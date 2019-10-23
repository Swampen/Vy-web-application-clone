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

        private readonly ILoginService _loginService;
        
        public AdminController(
            IVyService vyService,
            IZipSearchService zipSearchService,
            IStationService stationService,
            ILoginService loginService
        )
        {
            _vyService = vyService;
            _zipSearchService = zipSearchService;
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
            return Redirect("http://localhost:5000/");
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

            return Redirect("http://localhost:5000/");
        }

        [HttpDelete]
        public ActionResult DeleteTicket(int ticketId)
        {
            var session = (bool)Session["Auth"];
            if (session)
            {
                var success = _vyService.DeleteTicket(ticketId);
                return RedirectToAction("Tickets");
            }
            return Redirect("http://localhost:5000/");
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
            var session = (bool)Session["Auth"];
            if (session)
            {
                var stations = _stationService.getAllStations();
                return View(stations);
            }

            return Redirect("http://localhost:5000/");
        }



        [HttpPost]
        public ActionResult EditStation(StationDTO station)
        {
            var success = _vyService.ChangeStation(station);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Logout()
        {
            Session["Auth"] = false;
            Console.WriteLine("Logout");
            return Redirect("http://localhost:5000");
        }

//        public ActionResult RegisterNewAdmin(AdminUserDTO adminUserDto)
//        {
//            try
//            {
//                var session = (bool)Session["Auth"];
//                if (session)
//                {
//                    var superUser = (bool) Session["SuperUser"];
//                    if (superUser)
//                    {
//                        var UserCreated = _loginService.RegisterAdminUser(adminUserDto.Username,
//                            adminUserDto.Password, "ADMINISTRATOR");
//                        if (UserCreated)
//                        {
//                            return Redirect(Request.UrlReferrer.ToString());
//                        }
//                    }
//                    return Redirect(Request.UrlReferrer.ToString());
//                }
//
//                return Redirect(Request.UrlReferrer.ToString());
//            }
//            catch (Exception error)
//            {
//                Console.WriteLine(error);
//                throw;
//            }
//        }
    }
}