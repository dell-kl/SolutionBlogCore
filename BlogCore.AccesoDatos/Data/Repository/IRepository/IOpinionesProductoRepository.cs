﻿using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IOpinionesProductoRepository : IRepository<OpinionesProducto>
    {
        public void Update(OpinionesProducto opinionProducto);
    }
}
