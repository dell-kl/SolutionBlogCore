using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(
            ApplicationDbContext db    
        )
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity) => dbSet.Add(entity);

        public T? Get(int id) => dbSet.Find(id);


        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            string? includeProperties = null)
        {
           
            IQueryable<T> consulta = null!;

            if (filter != null)
                consulta = dbSet.Where(filter);
            else
                consulta = dbSet;

            if (includeProperties != null)
                consulta = consulta.Include(includeProperties);

            if ( orderBy != null )
                return orderBy!(consulta).ToList();

            return consulta.ToList();
        }

        public T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null
        )
        {
            IQueryable<T> consulta = null!;

            consulta = dbSet.Where(filter!);

            if (includeProperties != null)
                consulta = consulta.Include(includeProperties);

            return consulta.First();
        }

        public void Remove(int id) => dbSet.Remove(dbSet.Find(id)!);
        public void Remove(T entity) => dbSet.Remove(entity);
    }
}
