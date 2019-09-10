using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Vy.Service.Contracts;
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

        public ActionResult About()
        {
            ViewBag.Current = "About";
            ViewBag.Message = "Your application description page.";
            return View();
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
            
            // The next line is commented out to avoid creating a dummy view-file.
             //return View(service.GetExampleEntityDto());
             throw new NotImplementedException();
        }
    }
}