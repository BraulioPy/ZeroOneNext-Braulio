using Microsoft.AspNetCore.Mvc;
using Store.Web.Features.Products.Services;
using Store.Web.Features.Products.ViewModels;

namespace Store.Web.Features.Products.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index(string? q)
        {
            var products = _productService.GetAll(q);
            return View(products);
        }

        public IActionResult Details (int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Create()
        {
            return View(new ProductCreateViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var id = _productService.Create(vm);
            TempData["Success"] = "Producto Creado Correctamente";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
