using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ComentarioProductoRepository : Repository<ComentarioProducto>, IComentarioProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ComentarioProductoRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public void Update(ComentarioProducto comentarioProducto)
        {
            throw new NotImplementedException();
        }
    }
}
