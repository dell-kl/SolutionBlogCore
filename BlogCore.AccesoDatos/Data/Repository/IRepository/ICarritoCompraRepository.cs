using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface ICarritoCompraRepository : IRepository<CarritoCompra>
    {
        public void Update(CarritoCompra carritoCompra);
    }
}
