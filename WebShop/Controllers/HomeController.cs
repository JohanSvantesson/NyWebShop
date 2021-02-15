using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using WebShop.Data;
using WebShop.Models;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            viewModel.ProductCategories = _dbContext.ProductCategory.Select(dbCategory => new ProductCategoryViewModel()
            {
                Id = dbCategory.Id,
                Name = dbCategory.Name
            }).ToList();
            return View(viewModel);
        }

        
        [Authorize(Roles = "Admin, Product Manager")]
        public IActionResult Edit(int Id)
        {
            var viewModel = new HomeEditViewModel();
            var dbcat = _dbContext.ProductCategory.First(p => p.Id == Id);
            viewModel.Id = dbcat.Id;
            viewModel.Name = dbcat.Name;

            return View(viewModel);
        }
        
        [Authorize(Roles = "Admin, Product Manager")]
        [HttpPost]
        public IActionResult Edit(int Id, HomeEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbcat = _dbContext.ProductCategory.First(p => p.Id == Id);
                dbcat.Id = viewModel.Id;
                dbcat.Name = viewModel.Name;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        [Authorize(Roles = "Admin, Product Manager")]
        public IActionResult NewCategory()
        {
            var viewModel = new HomeNewCategoryViewModel();
            

            return View(viewModel);
        }
        [Authorize(Roles = "Admin, Product Manager")]
        [HttpPost]
        public IActionResult NewCategory(HomeNewCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbcat = new ProductCategory();
                _dbContext.Add(dbcat);
                dbcat.Name = viewModel.Name;

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
