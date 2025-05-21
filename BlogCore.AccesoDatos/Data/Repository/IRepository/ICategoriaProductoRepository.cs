using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoriaProductoRepository : IRepository<CategoriaProducto>
    {
        public void Update(CategoriaProducto categoriaProducto);
    }
}
