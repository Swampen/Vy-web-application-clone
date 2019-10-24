using BLL.Service.Contracts;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication_Vy.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult Logout()
        {
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

                return RedirectToAction("index", "admin");
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