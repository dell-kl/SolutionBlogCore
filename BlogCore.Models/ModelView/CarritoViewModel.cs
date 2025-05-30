namespace BlogCore.Models.ModelView
{
    public class CarritoViewModel
    {
        public int numeroProductos { set; get; } = 0;
        
        public Carrito carrito { set; get; } = new Carrito();

        public IEnumerable<CarritoCompra> carritoCompra { set; get; } = new List<CarritoCompra>();

        public IEnumerable<Producto> producto { set; get; } = new List<Producto>();
        
    }
}
