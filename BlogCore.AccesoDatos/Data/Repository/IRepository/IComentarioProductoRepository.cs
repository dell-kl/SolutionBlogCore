using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IComentarioProductoRepository : IRepository<ComentarioProducto>
    {
        public void Update(ComentarioProducto comentarioProducto);
    }
}
