using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Models.ModelView
{
    public class CarritoViewModel
    {
        public int numeroProductos { set; get; } = 0;
        
        public Carrito carrito { set; get; } = new Carrito();

        public IEnumerable<CarritoCompra> carritoCompra { set; get; } = new List<CarritoCompra>();

        public IEnumerable<Producto> producto { set; get; } = new List<Producto>();

        public decimal precioTotal { set; get; } = 0.0m;
        public List<decimal> productosPreciosConfigurados { set; get; } = [];
    
        public void configurarDatosCompra()
        {
            //vamos a realizar el seteo del precio final.
            if ( carritoCompra.Any() && producto.Any() )
            {
                foreach (CarritoCompra carritoCompra in carritoCompra)
                {
                    int cantidad = carritoCompra.carritoCompra_cantidad;
                    Producto? productoRegistado = producto.Where(n => n.producto_id.Equals(carritoCompra.Productoproducto_id)).FirstOrDefault();
                    decimal precio = (productoRegistado is not null)
                             ? ((bool)productoRegistado.producto_Esdescuento!) 
                                ? productoRegistado.producto_precioDescuento 
                                : productoRegistado.producto_precio
                             : 0.0m;

                    precio *= cantidad;
                    productosPreciosConfigurados.Add(precio);
                    precioTotal += precio;
                }
            }
        }
    }
}
