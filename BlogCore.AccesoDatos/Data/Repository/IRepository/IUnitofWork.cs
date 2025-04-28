using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IUnitofWork : IDisposable
    {
        public ICategoriaRepository Categoria { get; }

        public IArticuloRepository Articulo { get; }

        public ISliderRepository Slider { get; }

        public IUsuarioRepository Usuario { get; }

        public void Save();
    }
}
