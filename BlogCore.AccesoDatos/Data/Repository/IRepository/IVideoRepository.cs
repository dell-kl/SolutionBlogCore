using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IVideoRepository : IRepository<VideosProducto>
    {
        void Update(int id);
        void Update(VideosProducto video);
    }
}
