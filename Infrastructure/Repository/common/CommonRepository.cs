using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KanbanBoard.Api.Infrastructure.Repository.common
{
    public abstract class CommonRepository<TContext, T> : ICommonRepository<T> where T : class where TContext : DbContext
    {
        protected TContext _context;

        public CommonRepository(TContext context) => _context = context;

        public IQueryable<T> FindAll() => _context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
                _context.Set<T>()
                .Where(expression);


        public IQueryable<T> FindById(int id) =>
                    _context.Set<T>()
                    .Where(t => t.Equals(id));

        public IQueryable<T> FindById(string id) =>
                    _context.Set<T>()
                    .Where(t => t.Equals(id));

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public void SaveChanges() => _context.SaveChanges();

    }
}