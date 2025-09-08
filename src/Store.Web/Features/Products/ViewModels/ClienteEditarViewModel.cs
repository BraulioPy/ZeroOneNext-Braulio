namespace Store.Web.Features.Products.ViewModels
{
    public class ClienteEditarViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
