using Microsoft.AspNetCore.Mvc;

namespace WhatsOn.Controllers
{
    public class ContactController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
