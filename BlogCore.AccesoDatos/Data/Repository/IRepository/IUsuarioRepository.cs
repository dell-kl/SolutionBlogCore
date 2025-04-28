using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<ApplicationUser>
    {
        //vamos a tener que bloquear 
        public bool bloquearCuenta(string idCuenta);

        //vamos a tener que desbloquear la cuenta
        public bool desbloquearCuenta(string idCuenta);
    }
}
