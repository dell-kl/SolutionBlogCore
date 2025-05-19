using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccesoDatos.Data.SeederDB
{
    public class SeedDb : ISeedDb
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;
        private readonly ApplicationDbContext _context;

        public SeedDb(
            UserManager<ApplicationUser> userMngr,
            RoleManager<IdentityRole> rolMngr,
            ApplicationDbContext context
        )
        {
            _userManager = userMngr;
            _rolManager = rolMngr;
            _context = context;
        }

        public async Task InicializarDatos()
        {
            /*Definimos los roles vamos a crearlos... */
            if (! _rolManager.RoleExistsAsync(DefinicionRole.admin).GetAwaiter().GetResult())
                await _rolManager.CreateAsync(new IdentityRole(DefinicionRole.admin));

            if (!_rolManager.RoleExistsAsync(DefinicionRole.rgstr).GetAwaiter().GetResult())
                await _rolManager.CreateAsync(new IdentityRole(DefinicionRole.rgstr));

            if (!_rolManager.RoleExistsAsync(DefinicionRole.clt).GetAwaiter().GetResult())
                await _rolManager.CreateAsync(new IdentityRole(DefinicionRole.clt));

            /* Definimos a nuestro usuario principal, como administrador. */
            bool Existusuario = _userManager.Users.Any();
            ApplicationUser usuario = Activator.CreateInstance<ApplicationUser>();

            if ( !Existusuario )
            {
                usuario.UserName = "Admin";
                usuario.Nombres = "Dennis Ponce";
                usuario.Cedula = "1102935341";

                await _userManager.SetEmailAsync(usuario, "dennis-ponce07@outlook.com");
                await _userManager.CreateAsync(usuario, "TorakyLobo07@001001");
                await _userManager.AddToRoleAsync(usuario, "Administrador");
            }
        }

        public async Task InicializarSentenciaTSQL()
        {
            string ruta = "./../BlogCore.AccesoDatos/Data/SeederDB/Sentencias.sql";
            if ( File.Exists(ruta) )
            {
                var sentencias = File.ReadAllText(ruta);
                await _context.Database.ExecuteSqlRawAsync(sentencias);    
            }
        }
    }
}
