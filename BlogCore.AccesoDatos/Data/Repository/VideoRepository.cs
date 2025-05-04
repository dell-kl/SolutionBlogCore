using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class VideoRepository : Repository<VideosProducto>, IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(
            ApplicationDbContext context
        )
            : base(context)
        {
            _context = context;
        }

        public void Update(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VideosProducto video)
        {
            throw new NotImplementedException();
        }
    }
}
