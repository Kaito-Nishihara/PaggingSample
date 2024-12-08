using Microsoft.EntityFrameworkCore;
using PaggingSample.Entities;
using System.Linq.Expressions;
using X.PagedList;

namespace PaggingSample.Repository
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity> GetAsync(
         Expression<Func<TEntity, bool>> filter = null!,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
         string includeProperties = "");
        Task<IEnumerable<TEntity>> GetListAsync(
                 Expression<Func<TEntity, bool>> filter = null!,
                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
                 string includeProperties = "", int? page = null, int? size = null);
        bool GetByAny(Expression<Func<TEntity, bool>> filter = null!);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity newEntity);
        void Delete(Guid id);
        Task SaveAsync();
    }

    public class RepositoryBase<TEntity> where TEntity : class
    {
        internal readonly AppDbContext context;
        internal readonly DbSet<TEntity> dbSet;

        public RepositoryBase(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(
                 Expression<Func<TEntity, bool>> filter = null!,
                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
                 string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).SingleOrDefaultAsync();
            }
            else
            {
                return await query.SingleOrDefaultAsync();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(
                 Expression<Func<TEntity, bool>> filter = null!,
                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
                 string includeProperties = "", int? page = null, int? size = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (page.HasValue && size.HasValue)
            {
                if(orderBy != null)
                {
                    return await Task.FromResult(orderBy(query).ToPagedList((int)page,(int)size));
                }
                else
                {
                    return await Task.FromResult(query.ToPagedList((int)page, (int)size));
                }
            }
            if (orderBy is not null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual bool GetByAny(Expression<Func<TEntity, bool>> filter = null!)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual TEntity GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            dbSet.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public virtual void Update(TEntity newEntity)
        {
            dbSet.Update(newEntity);
            dbSet.Entry(newEntity).State = EntityState.Modified;
        }

        public virtual void Delete(Guid id)
        {
            var entity = dbSet.Find(id);
            if (entity is null)
            {
                throw new InvalidOperationException($"No entity found with ID {id}.");
            }
            dbSet.Remove(entity);
            dbSet.Entry(entity).State = EntityState.Deleted;
        }

        public virtual async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
