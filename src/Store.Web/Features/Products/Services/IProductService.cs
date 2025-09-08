using Store.Web.Features.Products.ViewModels;

namespace Store.Web.Features.Products.Services
{
    public interface IProductService
    {
        IReadOnlyList<ProductViewModel> GetAll(string? q);
        ProductViewModel? Get(int id);
        int Create(ProductCreateViewModel vm);
        bool Update(ProductEditViewModel vm);
        bool Delete(int id);
    }
}
