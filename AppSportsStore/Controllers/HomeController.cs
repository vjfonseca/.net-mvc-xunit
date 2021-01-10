using System.Linq;
using AppSportsStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppSportsStore.Controllers
{
    public class HomeController : Controller
    {
        public IStoreRepo repo { get; set; }
        public int PageSize { get; } = 3;
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
            return View(repo.Products.Skip((page - 1) * PageSize)
                                     .Take(PageSize));
        }
    }
}