using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
