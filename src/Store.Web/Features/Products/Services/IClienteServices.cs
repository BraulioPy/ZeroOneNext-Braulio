using Store.Web.Features.Products.ViewModels;

namespace Store.Web.Features.Products.Services
{
    public interface IClienteServices
    {
        IReadOnlyList<ClienteViewModel> GetAll(string? q);
        ClienteViewModel? Get(int id);
        int Create(ClienteCreateViewModel vm);
        bool Update(ClienteEditarViewModel vm);
        bool Delete(int id);
    }
}
