using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int error = 0)
        {
            ViewBag.Error = error != 0;

            return View("Index");
        }
    }
}
