using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class CarritoRepository : Repository<Carrito>, ICarritoRepository
    {
        private readonly ApplicationDbContext _db;

        public CarritoRepository(ApplicationDbContext db) 
        : base(db)
        {
            _db = db;
        }

    }
}
