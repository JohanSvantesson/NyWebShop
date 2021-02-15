using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;

namespace WebShop.ViewModels
{
    public class ViewProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<ViewProductViewModel> Products { get; set; } = new List<ViewProductViewModel>();
    }
    public class ProductViewProductViewModel
    {
        public List<ViewProductViewModel> Products { get; set; } = new List<ViewProductViewModel>();
    }
}



