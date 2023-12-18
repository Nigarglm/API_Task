using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace API_Task.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly DbSet<T> _table;
        private readonly AppContext _context;
       
        public Repository(AppDbContext context)
        {
            _table = context.Set<T>();
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public IQueryable<T> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null, 
            bool isDescending=false,
            int skip=0,
            int take=0,
            bool isTracking=true,
            params string[] includes)
        {
            var query = _table.AsQueryable();
            if(expression != null)
            {
                query=query.Where(expression);
            }
            if(orderExpression != null)
            {
                if (isDescending)
                {
                    query = query.OrderByDescending(orderExpression);
                }
                else
                {
                    query = query.OrderBy(orderExpression);
                }
            }
            if (skip != 0)
            {
                query= query.Skip(skip);
            }
            if (take != 0)
            {
                query = query.Take(take);
            }
            if (includes != null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query= query.Include(includes[i]);
                }
            }
            return isTracking?query:query.AsNoTracking();
        }

        public Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(e=>e.Id==id);

            return entity;
        }

        public async Task SaveChangesAsync(T entity)
        {
            await _context.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        Task<Category> IRepository<T>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
