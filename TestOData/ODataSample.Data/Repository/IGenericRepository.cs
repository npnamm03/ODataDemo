using System.Linq.Expressions;

namespace ODataSample.Repository
{

    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            string includeProperties = "");

        T GetByID(object id);

        void Insert(T entity);

        bool Delete(object id);

        void Delete(T entityToDelete);
        bool Update(object id, T entityToUpdate);
        void Update(T entityToUpdate);
        void Save();
    }


}
