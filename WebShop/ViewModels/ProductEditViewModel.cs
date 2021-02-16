using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.Data;

namespace WebShop.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Description { get; set; }

       
        [Required]
        public int Price { get; set; }

        
        public ProductCategory ProductCategory { get; set; }

        [Required]
        [Range(1,100)]
        public int SelectedCategoryId { get; set; }
        public List<SelectListItem> AllCategories { get; set; } = new List<SelectListItem>();
    }

   
}
