using System.Web.Mvc;

namespace Betting.Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}