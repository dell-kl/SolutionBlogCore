using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool bloquearCuenta(string idCuenta)
        {
            ApplicationUser? usuario = _db.ApplicationUser.Where(n => n.Id.Equals(idCuenta)).First();

            if ( usuario != null )
            {
                usuario.LockoutEnd = DateTime.Now;
                _db.Update(usuario);

                return true;
            }

            return false;
        }

        public bool desbloquearCuenta(string idCuenta)
        {
            ApplicationUser? usuario = _db.ApplicationUser.Where(n => n.Id.Equals(idCuenta)).First();
        
            if ( usuario != null )
            {
                usuario.LockoutEnd = null;
                _db.Update(usuario);

                return true;
            }

            return false;
        }
    }
}
