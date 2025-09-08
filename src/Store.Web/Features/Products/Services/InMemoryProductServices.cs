using System.Collections.Concurrent;
using Store.Web.Features.Products.Models;
using Store.Web.Features.Products.ViewModels;

namespace Store.Web.Features.Products.Services
{
    public class InMemoryProductServices : IProductService
    {
        private readonly ConcurrentDictionary<int,Product> _db = new();
        private int _seq = 0;
        public InMemoryProductServices() {
            Create(new ProductCreateViewModel { Name = "Laptop", Description = "Description 1", Price = 10.0m });
            Create(new ProductCreateViewModel { Name = "Mouse", Description = "Description 2", Price = 20.0m });
            Create(new ProductCreateViewModel { Name = "Keyboard", Description = "Description 3", Price = 30.0m });
        }
        public int Create(ProductCreateViewModel vm)
        {
            var id = Interlocked.Increment(ref _seq);
            var item = new Product { Id = id, Name = vm.Name, Description = vm.Description, Price = vm.Price };
            _db[id] = item;
            return id;
        }

        public bool Delete(int id) => _db.TryRemove(id, out _);

        public ProductViewModel? Get(int id) => _db.TryGetValue(id, out var vm) ? new
            ProductViewModel {
            Id = vm.Id,
            Name = vm.Name,
            Description = vm.Description,
            Price = vm.Price
        }
        : null;

        public IReadOnlyList<ProductViewModel> GetAll(string? q)
        {
            var items = _db.Values.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                items.Where(p => p.Name.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            return items.OrderBy(p => p.Name).Select(p => new ProductViewModel { Id = p.Id, Name = p.Name, Price = p.Price, Description = p.Description }).ToList();
        }
        public bool Update(ProductEditViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
