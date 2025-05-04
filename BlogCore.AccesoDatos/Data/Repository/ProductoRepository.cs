using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlogCore.AccesoDatos.Data.Repository
{

    [Authorize(Roles = "Administrador")]
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductoRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Producto producto) {

            Producto? productoDb = _dbContext.Producto.FirstOrDefault(u => u.producto_id == producto.producto_id);

            if ( productoDb is not null )
            {
                
                if ((bool)producto.producto_Esdescuento!)
                    productoDb.producto_descuento = 0;
                else
                    productoDb.producto_descuento = producto.producto_descuento;

                productoDb.producto_nombre = producto.producto_nombre;
                productoDb.producto_precio = producto.producto_precio;
                productoDb.producto_stock = producto.producto_stock;
                productoDb.producto_disponiblidad = producto.producto_disponiblidad;
                productoDb.producto_descripcion = producto.producto_descripcion;
                productoDb.producto_Esdescuento = producto.producto_Esdescuento;
                productoDb.producto_fechaCreacion = producto.producto_fechaCreacion;
                productoDb.producto_fechaModificacion = DateTime.Now;
                productoDb.CategoriaProductocategoriaProducto_id = producto.CategoriaProductocategoriaProducto_id;

                _dbContext.Update(productoDb); 
            }

        }
        
    }
}
