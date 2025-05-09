using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ComentarioArticuloRepository : Repository<ComentarioArticulo>, IComentarioArticuloRepository
    {

        private readonly ApplicationDbContext _context;
        public ComentarioArticuloRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public void Update(ComentarioArticulo comentarioArticulo)
        {

        }

    }
}
