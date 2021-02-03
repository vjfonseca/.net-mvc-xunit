using System.Linq;
using AppSportsStore.Models;
using AppSportsStore.Models.ViewModels;
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
        public IActionResult Index()
        {
            return View(repo.Products);
        }
        public IActionResult ProductsPagination(int page = 1)
        {
            return View(repo.Products.OrderBy(p => p.ProductID)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize));
        }
        public ViewResult IndexPagination(int productPage = 1) => View(new ProductsListViewModel
        {
            Products = repo.Products.OrderBy(p => p.ProductID)
                                          .Skip((productPage - 1) * PageSize)
                                          .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = repo.Products.Count()
            }
        });

    }
}