using AppSportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppSportsStore.Controllers
{
    public class HomeController : Controller
    {
        public IStoreRepo repo { get; set; }
        public HomeController(IStoreRepo _repo)
        {
            repo = _repo;
        }
        // public IActionResult Index() => View();
        // public IViewResult Index() 
        // return View(repo.products)
        public IActionResult Index()
        {
            return View(repo.Products);
        }
        public IActionResult ProductsPagination(int page = 1)
        {
            // return View()
            //int pageSize = 4;
            return null;
        }
    }
}