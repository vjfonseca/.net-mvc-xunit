using Microsoft.AspNetCore.Mvc;

namespace AppSportsStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}