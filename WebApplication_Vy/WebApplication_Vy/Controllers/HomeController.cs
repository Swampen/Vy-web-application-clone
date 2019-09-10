using System;
using System.Web.Mvc;
using WebApplication_Vy.Db;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVyService _vyService;

        public HomeController(IVyService vyService)
        {
            _vyService = vyService;
        }

        public ActionResult Index()
        {
            var db = new VyDbContext();
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
            _vyService.GetCustomerDtos();

            // The next line is commented out to avoid creating a dummy view-file.
            //return View(service.GetExampleEntityDto());
            throw new NotImplementedException();
        }
    }
}