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

        public void Update(Carrito carrito)
        {
            Carrito? carritoRegistrado = _db.Carrito.Where(c => c.carrito_id.Equals(carrito.carrito_id)).FirstOrDefault();
        
            if ( carritoRegistrado is not null )
            {
                carritoRegistrado.carrito_id = carrito.carrito_id;
                carritoRegistrado.carrito_sessionId = carrito.carrito_sessionId;
                carritoRegistrado.carrito_fechaModificacion = DateTime.Now;
                carritoRegistrado.IdentityUserId = carrito.IdentityUserId;

                _db.Carrito.Update(carritoRegistrado);
            }
        }
    }
}
