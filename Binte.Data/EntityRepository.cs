using Binte.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data
{
    public class EntityRepository<T> : IRepository<T> where T : BaseEntity
    {
        BinteContext _context;
        DbSet<T> _table;
        public EntityRepository(BinteContext context)
        {
            _context= context;
            _table=_context.Set<T>();
        }
        public bool DeleteData(T entity)
        {
            try
            {
                _table.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public T Get(int id)
        {
            return _table.FirstOrDefault(p => p.Id == id);
        }

        public List<T> GetAll(Expression<Func<T, bool>> sart)
        {
            return _table.Where(sart).ToList();
                
        }
        public List<T> GetAll()
        {
            return _table.ToList();

        }

        public bool InsertData(T entity)
        {
            try
            {
                if(entity is CreatableEntity e2)
                { e2.CreateDate= DateTime.Now; }
                _table.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateData(T entity)
        {
            try
            {
                _table.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
