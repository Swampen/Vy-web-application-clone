using System.Web.Http;

namespace WebApplication_Vy.Controllers
{
    public class WebDummyController : ApiController
    {
        // GET: api/student/5
        [System.Web.Mvc.HttpGet]
        public string something(int id)
        {
            return "value";
        }
    }
}