using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<ProductCategoryViewModel> ProductCategories { get; set; } = new List<ProductCategoryViewModel>();
    }

    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
