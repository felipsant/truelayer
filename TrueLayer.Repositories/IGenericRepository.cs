using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrueLayer.Repositories
{
    public interface IGenericRepository<T, TKey>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                   string includeProperties = "");
        T GetByID(object[] param);
        T GetByID(TKey id);
        void Insert(T entity);
        void Delete(TKey id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
    }
}
