using BLL.Service.Contracts;
using DAL.DTO;
using System;
using System.Web.Mvc;
using log4net;
using UTILS.Utils.Logging;

namespace WebApplication_Vy.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog Log = LogHelper.GetLogger();
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult Logout()
        {
            Log.Info(LogEventPrefixes.USER_LOGOUT + ": " + Session["Username"] + " has logged out");
            Session["Auth"] = false;
            Session["SuperAdmin"] = false;
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminUserDTO adminUserDto)
        {
            var login = _loginService.Login(adminUserDto);
            if (login)
            {
                Session["Auth"] = true;
                
                Console.WriteLine("login succeeded");
                if (_loginService.isSuperAdmin(adminUserDto.Username))
                {
                    Session["SuperAdmin"] = true;
                }
                Session["Username"] = adminUserDto.Username;

                return RedirectToAction("stations", "admin");
            }
            else
            {
                TempData["error"] = "Wrong username or password";
                Session["Auth"] = false;
                Session["SuperAdmin"] = false;
                return RedirectToAction("index", "home");
            }
        }

    }
}