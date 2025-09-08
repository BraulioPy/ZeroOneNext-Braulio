using System.ComponentModel.DataAnnotations;

namespace Store.Web.Features.Products.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required, StringLength(80)]
        public string Name { get; set; }
        [Range(0, 10000)]
        public decimal Price { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
    }
}
