using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IProductoRepository : IRepository<Producto>
    {
        //vamos agregar metodos de actualizacion
        public void Update(int id);

        public void Update(Producto producto);
    }
}
