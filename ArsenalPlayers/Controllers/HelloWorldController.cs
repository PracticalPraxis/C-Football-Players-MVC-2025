using Microsoft.AspNetCore.Mvc;

namespace ArsenalPlayers.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
