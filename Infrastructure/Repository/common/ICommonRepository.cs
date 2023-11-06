
using System.Linq.Expressions;
using System.Reflection;

namespace KanbanBoard.Api.Infrastructure.Repository.common
{
    public interface ICommonRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IQueryable<T> FindById(int id);
        IQueryable<T> FindById(string id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T? FindByPrimaryKey(object primaryKey);
        PropertyInfo? GetPrimaryKeyProperty();
    }
}