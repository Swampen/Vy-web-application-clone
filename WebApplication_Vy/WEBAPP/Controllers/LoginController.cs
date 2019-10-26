using System.Web.Mvc;
using BLL.Service.Contracts;
using DAL.DTO;
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

                Log.Info(LogEventPrefixes.USER_LOGIN + ": " + adminUserDto.Username + " has logged in");
                if (_loginService.isSuperAdmin(adminUserDto.Username))
                {
                    Log.Info(LogEventPrefixes.USER_LOGIN +
                             ": user: " + adminUserDto.Username + " is superadmin");
                    Session["SuperAdmin"] = true;
                }

                Session["Username"] = adminUserDto.Username;

                return RedirectToAction("stations", "admin");
            }

            Log.Warn(LogEventPrefixes.AUTHENTICATION_ERROR +
                     ": login failed for user: " + adminUserDto.Username + "wrong username or password");
            TempData["error"] = "Wrong username or password";
            Session["Auth"] = false;
            Session["SuperAdmin"] = false;
            return RedirectToAction("index", "home");
        }
    }
}