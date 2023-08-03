using Binte.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Data
{
    public interface IRepository<T> where T:BaseEntity
    {
        bool DeleteData(T entity);
        bool UpdateData(T entity);
        bool InsertData(T entity);
        T Get(int id);
        List<T> GetAll(Expression<Func<T, bool>> sart);
        List<T> GetAll();


    }
}
