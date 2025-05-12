using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class OpinionesProductoRepository : Repository<OpinionesProducto>, IOpinionesProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public OpinionesProductoRepository(ApplicationDbContext context) 
        : base(context)
        {
            _context = context;
        }
        public void Update(OpinionesProducto opinionProducto)
        {
            throw new NotImplementedException();
        }
    }
}
