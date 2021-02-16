using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ProductController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult AllProducts()
        {
            var viewModel = new ProductAllProductsViewModel();
            viewModel.Products = _dbContext.Product.Include(r => r.ProductCategory).Select(dbProducts => new AllProductsViewModel()
            {
                Id = dbProducts.Id,
                Name = dbProducts.Name,
                Description = dbProducts.Description,
                Price = dbProducts.Price,
                ProductCategory = dbProducts.ProductCategory
                

            }).ToList();

            return View(viewModel);
        }

        public IActionResult ViewTargetProduct(int Id)
        {
            var viewModel = new ProductViewTargetProduct();
            var dbProduct = _dbContext.Product.Include(r => r.ProductCategory)
                .First(r => r.Id == Id);

            viewModel.Id = dbProduct.Id;
            viewModel.Name = dbProduct.Name;
            viewModel.Description = dbProduct.Description;
            viewModel.Price = dbProduct.Price;
            viewModel.ProductCategory = dbProduct.ProductCategory;
            return View(viewModel);
        }
        public IActionResult ViewProducts(int Id)
        {
            var viewModel = new ViewProductViewModel();

            viewModel.Products = _dbContext.Product.Where(p => p.ProductCategory.Id == Id)
                            .Select(prod => new ViewProductViewModel
                            {
                                Id = prod.Id,
                                Description = prod.Description,
                                Name = prod.Name,
                                Price = prod.Price,
                                ProductCategory = prod.ProductCategory

                            }).ToList();

            return View(viewModel);
        }

        public IActionResult Search(string q)
        {
            var viewModel = new ProductSearchViewModel();

            viewModel.Products = _dbContext.Product
                .Include(r => r.ProductCategory)
                .Where(r => q == null || r.Name.Contains(q) || r.ProductCategory.Name.Contains(q))
                .Select(product => new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ProductCategory = product.ProductCategory

                }).ToList();
            return View(viewModel);
        }
        [Authorize(Roles = "Admin, Product Manager")]
        public IActionResult Edit(int Id)
        {
            var viewModel = new ProductEditViewModel();

            var dbProduct = _dbContext.Product.Include(r => r.ProductCategory).First(r => r.Id == Id);

            viewModel.SelectedCategoryId = dbProduct.ProductCategory.Id;
            viewModel.AllCategories = GetProductsListItems();
            viewModel.Name = dbProduct.Name;
            viewModel.Id = dbProduct.Id;
            viewModel.Description = dbProduct.Description;
            viewModel.Price = dbProduct.Price;


            return View(viewModel);
        }
        [Authorize(Roles = "Admin, Product Manager")]
        [HttpPost]
        public IActionResult Edit(int Id, ProductEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = _dbContext.Product.Include(p => p.ProductCategory)
                    .First(p => p.Id == Id);
                dbProduct.ProductCategory = _dbContext.ProductCategory.First(p => p.Id == viewModel.SelectedCategoryId);
                dbProduct.Id = viewModel.Id;
                dbProduct.Name = viewModel.Name;
                dbProduct.Description = viewModel.Description;
                dbProduct.Price = viewModel.Price;
                _dbContext.SaveChanges();
                return RedirectToAction("AllProducts");

            }

            viewModel.AllCategories = GetProductsListItems();
            return View(viewModel);
        }
        private List<SelectListItem> GetProductsListItems()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "..please select..." });

            list.AddRange(_dbContext.ProductCategory.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }));
            return list;
        }
        [Authorize(Roles = "Admin, Product Manager")]
        public IActionResult NewProduct()
        {
            var viewModel = new ProductNewProductViewModel();
            viewModel.AllCategories = GetProductsListItems();

            return View(viewModel);
        }
        [Authorize(Roles = "Admin, Product Manager")]
        [HttpPost]
        public IActionResult NewProduct(ProductNewProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = new Product();
                _dbContext.Add(dbProduct);
                dbProduct.ProductCategory = _dbContext.ProductCategory.First(r => r.Id == viewModel.SelectedCategoryId);
                dbProduct.Name = viewModel.Name;
                dbProduct.Description = viewModel.Description;
                dbProduct.Price = viewModel.Price;
                
                _dbContext.SaveChanges();
                return RedirectToAction("AllProducts");
            }

            viewModel.AllCategories = GetProductsListItems();

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
