﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;

namespace WebShop.ViewModels
{
    public class ProductAllProductsViewModel
    {
        public List<AllProductsViewModel> Products { get; set; } = new List<AllProductsViewModel>();
    }

    public class AllProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
