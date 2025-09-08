using System.ComponentModel.DataAnnotations;

namespace Store.Web.Features.Products.ViewModels
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        [Required]
        public int Id { get; set; }

    }
}
