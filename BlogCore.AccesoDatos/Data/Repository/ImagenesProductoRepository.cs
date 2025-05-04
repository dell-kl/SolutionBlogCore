using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ImagenesProductoRepository : Repository<ImagenesProducto>, IImagenesProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ImagenesProductoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ImagenesProductoRepository imagenProducto)
        {
            throw new NotImplementedException();
        }
    }
}
