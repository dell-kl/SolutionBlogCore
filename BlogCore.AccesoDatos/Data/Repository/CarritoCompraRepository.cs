using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class CarritoCompraRepository : Repository<CarritoCompra>, ICarritoCompraRepository
    {
        private readonly ApplicationDbContext _db;
        public CarritoCompraRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
