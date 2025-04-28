using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccesoDatos.Data.SeederDB
{
    public class SeedDb : ISeedDb
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;

        public SeedDb(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userMngr,
            RoleManager<IdentityRole> rolMngr
        )
        {
            _dbContext = dbContext;
            _userManager = userMngr;
            _rolManager = rolMngr;
        }

        public async void InicializarDatos()
        {
            if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                _dbContext.Database.Migrate();

            /*Definimos los roles vamos a crearlos... */
            if (! _rolManager.RoleExistsAsync(DefinicionRole.admin).GetAwaiter().GetResult())
                _rolManager.CreateAsync(new IdentityRole(DefinicionRole.admin)).GetAwaiter();

            if (!_rolManager.RoleExistsAsync(DefinicionRole.rgstr).GetAwaiter().GetResult())
                _rolManager.CreateAsync(new IdentityRole(DefinicionRole.rgstr)).GetAwaiter();

            if (!_rolManager.RoleExistsAsync(DefinicionRole.clt).GetAwaiter().GetResult())
                _rolManager.CreateAsync(new IdentityRole(DefinicionRole.clt)).GetAwaiter();

            /* Definimos a nuestro usuario principal, como administrador. */
            bool Existusuario = _userManager.Users.Any();

            if ( !Existusuario )
            {
                ApplicationUser usuario = new ApplicationUser() { 
                    UserName = "Admin",
                    Nombres = "Dennis Ponce",
                    Cedula = "1102935341",
                };
                _userManager
                    .SetEmailAsync(usuario, "dennis-ponce07@outlook.com")
                    .GetAwaiter()
                    .GetResult();
                _userManager
                    .CreateAsync(usuario, "TorakyLobo07@001001")
                    .GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(usuario, DefinicionRole.admin)
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
