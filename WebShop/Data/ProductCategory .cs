using System.ComponentModel.DataAnnotations;

namespace WebShop.Data
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}