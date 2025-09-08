using Microsoft.AspNetCore.Mvc;
using Store.Web.Features.Products.Services;
using Store.Web.Features.Products.ViewModels;

namespace Store.Web.Features.Products.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteServices _clienteService;
        public ClientesController(IClienteServices clienteService)
        {
            _clienteService = clienteService;
        }
        public IActionResult Index(string? q)
        {
            var clientes = _clienteService.GetAll(q);
            return View(clientes);
        }

        public IActionResult Details(int id)
        {
            var cliente = _clienteService.Get(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        public IActionResult Create()
        {
            return View(new ClienteCreateViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClienteCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var id = _clienteService.Create(vm);
            TempData["Success"] = "Cliente Creado Correctamente";
            return RedirectToAction(nameof(Details), new { id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var existing = _clienteService.Get(id);
            if (existing == null)
            {
                return NotFound();
            }
            _clienteService.Delete(id);
            TempData["Success"] = "Cliente Eliminado Correctamente";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var existing = _clienteService.Get(id);
            if (existing == null)
            {
                return NotFound();
            }
            var vm = new ClienteEditarViewModel
            {
                Id = existing.Id,
                Nombre = existing.Nombre,
                Email = existing.Email,
                Telefono = existing.Telefono,
                Direccion = existing.Direccion
            };
            return View(vm);
        }

    }
}
