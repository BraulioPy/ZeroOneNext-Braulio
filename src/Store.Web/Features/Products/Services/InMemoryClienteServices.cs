using Store.Web.Features.Products.Models;
using System.Collections.Concurrent;
using Store.Web.Features.Products.ViewModels;

namespace Store.Web.Features.Products.Services
{
    public class InMemoryClienteServices : IClienteServices
    {
        private readonly ConcurrentDictionary<int, Cliente> _db = new();
        private int _seq = 0;
        public InMemoryClienteServices()
        {
            Create(new ClienteCreateViewModel { Nombre = "Juan", Email = "juan@gmail.com", Direccion = "Veracruz Tapa chico #523", Telefono = "4412343321" });
            Create(new ClienteCreateViewModel {Nombre="Romero", Email="romero@gmail.com", Direccion = "Veracruz Tapa chico #523", Telefono = "4412343321" });
            Create(new ClienteCreateViewModel { Nombre="Lucrecio",Email="lucrecio@gmail.com", Direccion = "Veracruz Tapa chico #523", Telefono = "4412343321" });
        }
        public int Create(ClienteCreateViewModel vm)
        {
            var id = Interlocked.Increment(ref _seq);
            var item = new Cliente { Id = id, Nombre = vm.Nombre, Email = vm.Email, Direccion = vm.Direccion, Telefono= vm.Telefono };
            _db[id] = item;
            return id;
        }

        public bool Delete(int id) => _db.TryRemove(id, out _);
        public ClienteViewModel? Get(int id) => _db.TryGetValue(id, out var vm) ? new
            ClienteViewModel
        {
            Id = vm.Id,
            Nombre = vm.Nombre,
            Email = vm.Email,
            Telefono = vm.Telefono,
            Direccion = vm.Direccion,
            FechaRegistro = vm.FechaRegistro,
            Activo = vm.Activo
        }
        : null;


        public IReadOnlyList<ClienteViewModel> GetAll(string? q)
        {
            var items = _db.Values.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                items.Where(p => p.Nombre.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            return items.OrderBy(p => p.Nombre).Select(p => new ClienteViewModel { Id = p.Id, Nombre = p.Nombre, Email = p.Email, Telefono = p.Telefono, Direccion = p.Direccion, FechaRegistro = p.FechaRegistro, Activo = p.Activo}).ToList();
        }


        public bool Update(ClienteEditarViewModel vm)
        {
            if (_db.TryGetValue(vm.Id, out var existing))
            {
                existing.Nombre = vm.Nombre;
                existing.Email = vm.Email;
                existing.Telefono = vm.Telefono;
                existing.Direccion = vm.Direccion;
                existing.Activo = vm.Activo;
                return true;
            }
            return false;
        }
    }
}
