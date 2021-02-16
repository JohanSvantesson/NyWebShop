using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.ViewModels
{
    public class HomeNewCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Name { get; set; }
    }
}
